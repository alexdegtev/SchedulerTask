using GanttChart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace SchedulerTask_2._0
{
    public partial class Form1 : Form
    {

        OverlayPainter _mOverlay = new OverlayPainter();

        ProjectManager _mManager = null;
        public Form1()
        {
            InitializeComponent();

            _mManager = new ProjectManager();
            chart1.Init(_mManager);
            chart1.CreateTaskDelegate = delegate() { return new MyTask(_mManager); };
            // Init the rest of the UI
           // _InitExampleUI();           
        }

        void _mChart_TaskSelected(object sender, TaskMouseEventArgs e)
        {
            propertyGrid1.SelectedObjects = chart1.SelectedTasks.Select(x => _mManager.IsPart(x) ? _mManager.SplitTaskOf(x) : x).ToArray();
            listView1.Items.Clear();
            listView1.Items.AddRange(_mManager.ResourcesOf(e.Task).Select(x => new ListViewItem(((MyResource)x).Name)).ToArray());
        }

        void _mChart_TaskMouseOut(object sender, TaskMouseEventArgs e)
        {
            lblStatus.Text = "";
            chart1.Invalidate();
        }

        void _mChart_TaskMouseOver(object sender, TaskMouseEventArgs e)
        {
            lblStatus.Text = string.Format("{0} to {1}", _mManager.GetDateTime(e.Task.Start).ToLongDateString(), _mManager.GetDateTime(e.Task.End).ToLongDateString());
            chart1.Invalidate();
        }


        /// <summary>
        /// загрузить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Построить расписание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            

            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var task = new MyTask(_mManager) { Name = string.Format("New Task {0}", i.ToString()) };
                _mManager.Add(task);
                _mManager.SetStart(task, rand.Next(5));
                _mManager.SetDuration(task, rand.Next(30));
            }

            var task1 = new MyTask(_mManager) { Name = "My Test Task" };
            _mManager.Add(task1);
            _mManager.SetStart(task1, 1);
            _mManager.SetDuration(task1, 100 / 24);
        }
        
        /// <summary>
        /// Визуализировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            chart1.Init(_mManager);
            chart1.CreateTaskDelegate = delegate() { return new MyTask(_mManager); };

            chart1.TaskSelected += new EventHandler<TaskMouseEventArgs>(_mChart_TaskSelected);

            // Set Time information
            _mManager.TimeScale = TimeScale.Day;
            _mManager.Start = DateTime.Parse("2016, 01, 01");//время начала 
            var span = DateTime.Parse("2016, 01, 21") - _mManager.Start;//директивный срок         


            _mManager.Now = (int)Math.Round(span.TotalDays); // set the "Now" marker at the correct date
            chart1.TimeScaleDisplay = TimeScaleDisplay.DayOfMonth; // Set the chart to display days of week in header
            
        }
        /// <summary>
        /// Анализ/ поиск ошибок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }

      /*  private void propertyGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (TaskGridView.SelectedRows.Count > 0)
            {
                var task = TaskGridView.SelectedRows[0].DataBoundItem as Task;
                chart1.ScrollTo(task);
            }
        }
       

        #region Sidebar

        private void _mDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            _mManager.Start = _mStartDatePicker.Value;
            var span = DateTime.Today - _mManager.Start;
            _mManager.Now = (int)Math.Round(span.TotalDays);
            if (_mManager.TimeScale == TimeScale.Week) _mManager.Now = (_mManager.Now % 7) * 7;
            chart1.Invalidate();
        }

        private void _mPropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            chart1.Invalidate();
        }

        private void _mNowDatePicker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan span = _mNowDatePicker.Value - _mStartDatePicker.Value;
            _mManager.Now = span.Days + 1;
            if (_mManager.TimeScale == TimeScale.Week) _mManager.Now = _mManager.Now / 7 + (_mManager.Now % 7 > 0 ? 1 : 0);
            chart1.Invalidate();
        }

        private void _mScrollDatePicker_ValueChanged(object sender, EventArgs e)
        {
            chart1.ScrollTo(_mScrollDatePicker.Value);
            chart1.Invalidate();
        }

        private void _mTaskGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (TaskGridView.SelectedRows.Count > 0)
            {
                var task = TaskGridView.SelectedRows[0].DataBoundItem as Task;
                _mChart.ScrollTo(task);
            }
        }

        #endregion Sidebar
      
         */

    }

    #region overlay painter
    /// <summary>
    /// An example of how to encapsulate a helper painter for painter additional features on Chart
    /// </summary>
    /// 

    public class OverlayPainter
    {
        /// <summary>
        /// Hook such a method to the chart paint event listeners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartOverlayPainter(object sender, ChartPaintEventArgs e)
        {
            // Don't want to print instructions to file
            if (this.PrintMode) return;

            var g = e.Graphics;
            var chart = e.Chart;

            // Demo: Static billboards begin -----------------------------------
            // Demonstrate how to draw static billboards
            // "push matrix" -- save our transformation matrix
            e.Chart.BeginBillboardMode(e.Graphics);

            // draw mouse command instructions
            int margin = 300;
            int left = 20;
            var color = chart.HeaderFormat.Color;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("THIS IS DRAWN BY A CUSTOM OVERLAY PAINTER TO SHOW DEFAULT MOUSE COMMANDS.");
            builder.AppendLine("*******************************************************************************************************");
            builder.AppendLine("Left Click - Select task and display properties in PropertyGrid");
            builder.AppendLine("Left Mouse Drag - Change task starting point");
            builder.AppendLine("Right Mouse Drag - Change task duration");
            builder.AppendLine("Middle Mouse Drag - Change task complete percentage");
            builder.AppendLine("Left Doubleclick - Toggle collaspe on task group");
            builder.AppendLine("Right Doubleclick - Split task into task parts");
            builder.AppendLine("Left Mouse Dragdrop onto another task - Group drag task under drop task");
            builder.AppendLine("Right Mouse Dragdrop onto another task part - Join task parts");
            builder.AppendLine("SHIFT + Left Mouse Dragdrop onto another task - Make drop task precedent of drag task");
            builder.AppendLine("ALT + Left Dragdrop onto another task - Ungroup drag task from drop task / Remove drop task from drag task precedent list");
            builder.AppendLine("SHIFT + Left Mouse Dragdrop - Order tasks");
            builder.AppendLine("SHIFT + Middle Click - Create new task");
            builder.AppendLine("ALT + Middle Click - Delete task");
            builder.AppendLine("Left Doubleclick - Toggle collaspe on task group");
            var size = g.MeasureString(builder.ToString(), e.Chart.Font);
            var background = new Rectangle(left, chart.Height - margin, (int)size.Width, (int)size.Height);
            background.Inflate(10, 10);
            g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(background, Color.LightYellow, Color.Transparent, System.Drawing.Drawing2D.LinearGradientMode.Vertical), background);
            g.DrawRectangle(Pens.Brown, background);
            g.DrawString(builder.ToString(), chart.Font, color, new PointF(left, chart.Height - margin));


            // "pop matrix" -- restore the previous matrix
            e.Chart.EndBillboardMode(e.Graphics);
            // Demo: Static billboards end -----------------------------------
        }

        public bool PrintMode { get; set; }
    }
    #endregion overlay painter    


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
        public new float Complete { get { return base.Complete; } set { Manager.SetComplete(this, value); } }
    }
    #endregion custom task and resource
}
