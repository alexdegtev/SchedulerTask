using Builder.Equipment;
using Builder.Events;
using Builder.Front.Sorting;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;
using System;
using System.Collections.Generic;

namespace Builder.Front.Building
{
    /// <summary>
    /// Класс постороение фронта
    /// </summary>
    public class DefaultBuilder : IBuilder
    {
        private List<IParty> party;
        private List<IOperation> operations;
        private ISorter frontSorter;

        public DefaultBuilder(List<IParty> party, ISorter frontSorter)
        {
            this.party = party;

            // Получение операций из партий
            operations = new List<IOperation>();
            foreach (IParty i in party)
            {
                TreePartyIterator partyIterator = i.GetIterator(i);
                while (partyIterator.Next())
                {
                    operations.AddRange(partyIterator.Current.GetPartyOperations());
                }
            }

            this.frontSorter = frontSorter;
        }

        public void Build()
        {
            EventList events = new EventList();
            List<IOperation> operationsTmp = new List<IOperation>();
            foreach (IOperation operation in operations)
            {
                operationsTmp.Add(operation);
            }
            foreach (IParty i in party)
            {
                events.Add(new Event(i.GetStartTimeParty()));
            }
            while (operationsTmp.Count != 0) 
            {
                while (events.Count != 0)
                {
                    List<IOperation> front = new List<IOperation>();

                    // Формирование фронта
                    foreach (IOperation operation in operationsTmp)
                    {
                        if (!operation.IsEnabled() && operation.PreviousOperationIsEnd(events[0].Time) &&
                            DateTime.Compare(operation.GetParty().GetStartTimeParty(), events[0].Time) <= 0)
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
                            operationsTmp.Remove(operation);
                        }

                        events.Add(new Event(operationTime));
                    }

                    events.RemoveFirst();
                }
                if (operationsTmp.Count != 0)
                {
                    DateTime newStartData = new DateTime();
                    foreach (IParty i in party)
                    {
                        if (i.GetEndTimeParty()>newStartData)
                        {
                            newStartData = i.GetEndTimeParty();
                        }
                    }
                    double hours = 0;
                    foreach (IOperation operation in operationsTmp)
                    { 
                        hours = hours + ((operation.GetDuration().TotalHours * 24)/(operation.GetEquipment().GetTimeWorkInTwentyFourHours().TotalHours));
                    }
                    DateTime newEndData = newStartData + (new TimeSpan((int)hours, 0, 0));
                    events.Add(new Event(newStartData));
                    IO.Reader.UpdateCalendars(newStartData, newEndData);
                }
            }
        }
    }
}
