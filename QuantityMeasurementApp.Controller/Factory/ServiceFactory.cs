using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Controller.Controllers;
using QuantityMeasurementApp.Repository.Interface;
using QuantityMeasurementApp.Repository.Services;

namespace QuantityMeasurementApp.Controller.Factory
{
    public class ServiceFactory
    {
        private readonly IQuantityMeasurementRepository _repository;
        private readonly IQuantityMeasurementService _service;
        private readonly QuantityMeasurementController _controller;

        public ServiceFactory()
        {
            _repository = QuantityMeasurementCacheRepository.GetInstance();

            _service = new QuantityMeasurementServiceImpl(_repository);

            _controller = new QuantityMeasurementController(_service);
        }

        public QuantityMeasurementController GetController()
        {
            return _controller;
        }
        public IQuantityMeasurementRepository GetRepository()
        {
            return new QuantityMeasurementDatabaseRepository();
        }

        public IQuantityMeasurementService GetService()
        {
            return new QuantityMeasurementServiceImpl(GetRepository());
        }
    
    }
}