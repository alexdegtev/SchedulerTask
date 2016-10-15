using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            List<DateTime> events = new List<DateTime>();
            foreach (Party i in party)
            {
                events.Add(i.getStartTimeParty());
            }

            while (events.Count != 0)
            {
                List<IOperation> front = new List<IOperation>();

                // Формирование фронта
                foreach (IOperation operation in operations)
                {
                    if (!operation.IsEnabled() && operation.PreviousOperationIsEnd(events[0]) &&
                        DateTime.Compare(operation.GetParty().getStartTimeParty(), events[0]) <= 0)
                    {
                        DateTime operationTime;
                        SingleEquipment equipment;
                        if (EquipmentManager.IsFree(events[0], operation, out operationTime, out equipment))
                        {
                            front.Add(operation);
                        }
                        else
                        {
                            events.Add(operationTime);
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

                    if (EquipmentManager.IsFree(events[0], operation, out operationTime, out equipment))
                    {
                        operation.SetOperationInPlan(events[0], operationTime, equipment);
                        equipment.OccupyEquip(events[0], operationTime);
                    }

                    events.Add(operationTime);
                }

                events.RemoveAt(0);
            }

        }
    }
}
