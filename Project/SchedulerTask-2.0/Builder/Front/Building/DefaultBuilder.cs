using Builder.Equipment;
using System;
using System.Collections.Generic;
using Builder.Events;
using Builder.Front.Sorting;

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
                TreeIterator partyIterator = i.GetIterator(i);
                while (partyIterator.Next())
                {
                    operations.AddRange(partyIterator.Current.GetPartyOperations());
                }
            }

            //this.equipmentManager = equipmentManager;
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
            foreach (Party i in party)
            {
                events.Add(new Event(i.GetStartTimeParty()));
            }

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

        }
    }
}
