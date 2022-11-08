using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;

namespace ProductManagement.DAL.Constracts
{
   public interface IDeliveryTonerRepository : IRepository<DeliveryToner>
   {
      Task<DeliveryToner> GetDeliveryTonerByKey(int key);

      // to check that database contain current month delivery toner or not.
      // datetime will be splite for month and year only.
      Task<IEnumerable<DeliveryTonerDto>> GetDeliveryTonerByDeliveryDate();

      Task<IEnumerable<DeliveryTonerDto>> GetDeliveryTonerByMachineId(int machineId);

      DeliveryToner GetLastDeliveryByMachineId(int machineId);

      Task<IEnumerable<DeliveryToner>> GetDeliveryToners();
   }
}
