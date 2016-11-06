using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Builder.Events;
using Builder.Front.Sorting;
using Builder.IO;

namespace Builder.Front.Building
{
    /// <summary>
    /// Класс постороение фронта
    /// </summary>
    public class DefaultBuilder : IBuilder
    {
        private List<Party> party;
        private List<IOperation> operations;
        private ISorter frontSorter;
        //private EquipmentManager equipmentManager;

        public DefaultBuilder(List<Party> party, /*EquipmentManager equipmentManager*/ ISorter frontSorter)
        {
            this.party = party;

            // Получение операций из партий
            operations = new List<IOperation>();
            foreach (Party i in party)
            {
                TreeIterator partyIterator = i.getIterator(i);
                while (partyIterator.next())
                {
                    operations.AddRange(partyIterator.Current.getPartyOperations());
                }
            }

            //this.equipmentManager = equipmentManager;
            this.frontSorter = frontSorter;
        }

        public void Build()
        {
            EventList events = new EventList();
            List<IOperation> operations_tmp = new List<IOperation>();
            foreach (IOperation operation in operations)
            {
                operations_tmp.Add(operation);
            }
            foreach (Party i in party)
            {
                events.Add(new Event(i.getStartTimeParty()));
            }
            while (operations_tmp.Count != 0) 
            {
                while (events.Count != 0)
                {
                    List<IOperation> front = new List<IOperation>();

                    // Формирование фронта
                    foreach (IOperation operation in operations_tmp)
                    {
                        if (!operation.IsEnabled() && operation.PreviousOperationIsEnd(events[0].Time) &&
                            DateTime.Compare(operation.GetParty().getStartTimeParty(), events[0].Time) <= 0)
                        {
                            DateTime operationTime;
                            SingleEquipment equipment;
                            if (EquipmentManager.IsFree(events[0].Time, operation, out operationTime, out equipment))
                            {
                                front.Add(operation);
                            }
                            else
                            {
                                events.Add(new Event(operationTime));
                            }
                        }
                    }

                    // Сортировка фронта
                    frontSorter.Sort(front);

                    // Назначение операций
                    foreach (IOperation operation in front)
                    {
                        DateTime operationTime;
                        SingleEquipment equipment;

                        if (EquipmentManager.IsFree(events[0].Time, operation, out operationTime, out equipment))
                        {
                            operation.SetOperationInPlan(events[0].Time, operationTime, equipment);
                            equipment.OccupyEquip(events[0].Time, operationTime);
                            operations_tmp.Remove(operation);
                        }

                        events.Add(new Event(operationTime));
                    }

                    events.RemoveFirst();
                }
                if (operations_tmp.Count != 0)
                {
                    DateTime new_start_data = new DateTime();
                    DateTime new_end_data ;
                    foreach (Party i in party)
                    {
                        if (i.getEndTimeParty()>new_start_data)
                        {
                            new_start_data = i.getEndTimeParty();
                        }
                    }
                    double hours = 0;
                    foreach (IOperation operation in operations_tmp)
                    { 
                        hours = hours + ((operation.GetDuration().TotalHours * 24)/(operation.GetEquipment().GetTimeWorkInTwentyFourHours().TotalHours));
                    }
                    new_end_data = new_start_data + (new TimeSpan((int)hours, 0, 0));
                    events.Add(new Event(new_start_data));
                    Builder.IO.Reader.UpdateCalendars(new_start_data, new_end_data);
                }
            }

        }
    }
}
