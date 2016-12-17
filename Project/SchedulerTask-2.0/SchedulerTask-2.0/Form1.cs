using Builder.Front;
using CommonTypes.Decision;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;
using Debugger.Exceptions;
using Debugger.FindExceptions;
using GanttChart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace SchedulerTask_2._0
{
    public partial class Form1 : Form
    {

        //OverlayPainter _mOverlay = new OverlayPainter();

        ProjectManager _mManager = null;

        /// <summary>
        /// Путь к исходным данным
        /// </summary>
        private string folderName = null;


        public Form1()
        {
            InitializeComponent();

            _mManager = new ProjectManager();
            chart1.BarWidth = 40;

            // Set the help text description for the FolderBrowserDialog.
            folderBrowserDialog1.Description =
                "Выберите директорию с входными файлами";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            folderBrowserDialog1.ShowNewFolderButton = false;

        }

        protected override void OnResize(EventArgs e) { base.OnResize(e); this.Invalidate(); }

        void _mChart_TaskSelected(object sender, TaskMouseEventArgs e)
        {
            propertyGrid1.SelectedObjects = chart1.SelectedTasks.Select(x => _mManager.IsPart(x) ? _mManager.SplitTaskOf(x) : x).ToArray();            

        }

        /// <summary>
        /// Выбрать директорию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.folderName = selectedPath();
        }
        /// <summary>
        /// Построить расписание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {


            /*Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var task = new MyTask(_mManager) { Name = string.Format("New Task {0}", i.ToString()) };
                _mManager.Add(task);
                _mManager.SetStart(task, rand.Next(5));
                _mManager.SetDuration(task, rand.Next(30));
            }
            */
            buildSchedule(folderName);
            if (String.IsNullOrEmpty(folderName))
            {
                return;
            }
            if (!(checkPath("tech.xml")) && !(checkPath("system.xml")))
            {
                return;
            }

            addTasksAndInitTimeHeader();

            chart1.Init(_mManager);
            chart1.CreateTaskDelegate = delegate() { return new MyTask(_mManager); };
            chart1.TimeScaleDisplay = TimeScaleDisplay.DayOfMonth; // Set the chart to display days of week in header
            _mManager.TimeScale = TimeScale.Day;
            chart1.TaskSelected += new EventHandler<TaskMouseEventArgs>(_mChart_TaskSelected);
        }

        /// <summary>
        /// Визуализировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Анализ/ поиск ошибок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }

        ///Меню.
        private void выбратьДиректориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderName = selectedPath();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            buildSchedule(folderName);
            if (String.IsNullOrEmpty(folderName))
            {
                return;
            }
            if (!(checkPath("tech.xml")) && !(checkPath("system.xml")))
            {                
                return;
            }
            addTasksAndInitTimeHeader();

            chart1.Init(_mManager);
            chart1.CreateTaskDelegate = delegate() { return new MyTask(_mManager); };
            chart1.TimeScaleDisplay = TimeScaleDisplay.DayOfMonth; // Set the chart to display days of week in header
            _mManager.TimeScale = TimeScale.Day;
            chart1.TaskSelected += new EventHandler<TaskMouseEventArgs>(_mChart_TaskSelected);
        }

        /// <summary>
        /// Загрузить готовое расписание.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void зАгрузитьРасписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderName = selectedPath();
            if (!checkPath("tech+solution.xml"))
            {
                MessageBox.Show("Директория не содержит файл tech+solution.xml");
                return;
            }            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ///Вспомогательные методы//



        private string selectedPath()
        {
            string folderPath = "";
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }
            folderPath += "\\";
            return folderPath;
        }//selectedPath

        private void buildSchedule(string folderName)
        {
            if (String.IsNullOrEmpty(folderName))
            {
                // Initializes the variables to pass to the MessageBox.Show method.

                string message = "Вы не выбрали директорию с исходными данными. Выбрать сейчас?";
                string caption = "Не выбрана директория";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.folderName = selectedPath();
                }
                else
                {
                    return;
                }
            }
            
            if (!(checkPath("tech.xml")) && !(checkPath("system.xml"))){
                MessageBox.Show("Директория не содержит указанных файлов");
                return;
            }
            //MessageBox.Show(folderName);

            Builder.IO.Reader.SetFolderPath(folderName);
            Builder.IO.Reader.ReadData(out parties, out operations, out equipment);

            FrontBuilding frontBuilding = new FrontBuilding(parties);
            frontBuilding.Build();

            Builder.IO.Writer writer = null;
            try
            {
                writer = new Builder.IO.Writer(folderName, folderName);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                System.Environment.Exit(1);
            }
            catch (System.IO.FileNotFoundException) //игнорируем ошибку т.к. файл создается райтером
            {
            }
            writer.WriteData(parties);

            Debugger.IO.Reader reader = new Debugger.IO.Reader(folderName, folderName);
            reader.ReadData(out decisions);

            ExceptionsSearch search = new ExceptionsSearch(operations, equipment, decisions, parties);
            exceptions = search.Execute();

            Debugger.IO.Writer writerD = new Debugger.IO.Writer(folderName);
            writerD.WriteLog(exceptions);


        }//buildSchedule

        /// <summary>
        /// Добавить операции и инициировать временную шкалу
        /// </summary>
        private void addTasksAndInitTimeHeader()
        {

            // Set Time information
            _mManager.TimeScale = TimeScale.Day;
            _mManager.Start = parties[0].GetStartTimeParty();//DateTime.Parse("2016, 01, 01");//время начала 
            //MessageBox.Show(parties[0].GetStartTimeParty().ToString());
            var span = parties[0].GetEndTimeParty() - _mManager.Start;//DateTime.Parse("2016, 01, 21") - _mManager.Start;//директивный срок   

            _mManager.Now = (int)Math.Round(span.TotalDays) -1 ; // set the "Now" marker at the correct date


            _mManager.ClearAll();

            foreach (IDecision decision in decisions)
            {
                var task = new MyTask(_mManager) { Name = decision.GetOperation().GetName() + " id= " + decision.GetOperation().GetId(), EqID = decision.GetEquipment().ToString() };
                _mManager.Add(task);
                var startTime = decision.GetStartTime() - _mManager.Start;
                var endTime = decision.GetEndTime() - decision.GetStartTime();
                _mManager.SetStart(task, (int)Math.Round(startTime.TotalDays));
                _mManager.SetDuration(task, (int)Math.Round(endTime.TotalDays));
            }

            //var task1 = new MyTask(_mManager) { Name = "My Test Task" };
            //_mManager.Add(task1);            
            //_mManager.SetStart(task1, 15);
            //_mManager.SetDuration(task1, 6);
        }//add tasks

        private void scheduleAnalysis()
        {
            //_mManager.AddCritical(task1);
            //_mManager.
        }

        private bool checkPath(string fileName)
        {
            if (File.Exists(this.folderName + fileName))
            {
                return true;
            }
            return false;
        }



        private FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        private OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();        

        List<IParty> parties = new List<IParty>();
        Dictionary<int, IOperation> operations = new Dictionary<int, IOperation>();
        Dictionary<int, IEquipment> equipment = new Dictionary<int, IEquipment>();
        List<IDecision> decisions = null;
        List<IException> exceptions = null;

        private void button4_Click_1(object sender, EventArgs e)
        {
            //Process.Start("../../../BuilderConsole/bin/Debug/BuilderConsole.exe");
            //this.Close();
        }
        
    }

    #region custom task and resource
    /// <summary>
    /// A custom resource of your own type (optional)
    /// </summary>
    [Serializable]
    public class MyResource
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// A custom task of your own type deriving from the Task interface (optional)
    /// </summary>
    [Serializable]
    public class MyTask : Task
    {
        public MyTask(ProjectManager manager)
            : base()
        {
            Manager = manager;
        }

        private ProjectManager Manager { get; set; }

        public new int Start { get { return base.Start; } set { Manager.SetStart(this, value); } }
        public new int End { get { return base.End; } set { Manager.SetEnd(this, value); } }
        public new int Duration { get { return base.Duration; } set { Manager.SetDuration(this, value); } }

    }
    #endregion custom task and resource
}
