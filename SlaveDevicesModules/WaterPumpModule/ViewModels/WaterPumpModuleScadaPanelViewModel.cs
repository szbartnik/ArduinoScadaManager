using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleScadaPanelViewModel : SlaveModuleScadaPanelViewModelBase
    {
        #region Commands

        public RelayCommand ReadCoilsCommand { get; private set; }
        public RelayCommand ReadInputsCommand { get; private set; }
        public RelayCommand ReadHoldingRegistersCommand { get; private set; }
        public RelayCommand ReadInputRegistersCommand { get; private set; }

        public RelayCommand WriteSingleCoilCommand { get; private set; }
        public RelayCommand WriteSingleRegisterCommand { get; private set; }
        public RelayCommand WriteMultipleCoilsCommand { get; private set; }
        public RelayCommand WriteMultipleRegistersCommand { get; private set; }

        #endregion

        public WaterPumpModuleScadaPanelViewModel(
            IModbusTransferManager modbusTransferManager, 
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcess)
            : base(modbusTransferManager, masterModuleProcess, slaveModuleProcess)
        {
            ReadCoilsCommand = new RelayCommand(() => ReadCoilsRequest((ushort) RequestAddress, (ushort) ReadRequestNumOfElements));
            ReadInputsCommand = new RelayCommand(() => ReadInputsRequest((ushort) RequestAddress, (ushort) ReadRequestNumOfElements));
            ReadHoldingRegistersCommand = new RelayCommand(() => ReadHoldingRegistersRequest((ushort) RequestAddress, (ushort) ReadRequestNumOfElements));
            ReadInputRegistersCommand = new RelayCommand(() => ReadInputRegistersRequest((ushort) RequestAddress, (ushort) ReadRequestNumOfElements));
        
            WriteSingleCoilCommand = new RelayCommand(() => WriteSingleCoilRequest((ushort) RequestAddress, WriteSingleCoilSelectedOption == 0));
            WriteSingleRegisterCommand = new RelayCommand(() => WriteSingleRegisterRequest((ushort) RequestAddress, (ushort) WriteSingleRegisterContent));
            //WriteMultipleCoilsCommand = new RelayCommand(() => WriteMultipleCoilsRequest((ushort) RequestAddress, ));
            //WriteMultipleRegistersCommand = new RelayCommand(() => WriteMultipleRegistersRequest((ushort) RequestAddress, ));
        }

        protected override void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            LastResponse = modbusTransferData.ToString();
        }

        #region Properties

        public string LastResponse
        {
            get { return _lastResponse; }
            set
            {
                _lastResponse = value; 
                OnPropertyChanged();
            }
        }
        private string _lastResponse;

        public int RequestAddress
        {
            get { return _requestAddress; }
            set
            {
                _requestAddress = value; 
                OnPropertyChanged();
            }
        }
        private int _requestAddress;

        public int ReadRequestNumOfElements
        {
            get { return _readRequestNumOfElements; }
            set
            {
                _readRequestNumOfElements = value;
                OnPropertyChanged();
            }
        }
        private int _readRequestNumOfElements;

        public int WriteSingleCoilSelectedOption
        {
            get { return _writeSingleCoilSelectedOption; }
            set
            {
                _writeSingleCoilSelectedOption = value; 
                OnPropertyChanged();
            }
        }
        private int _writeSingleCoilSelectedOption;

        public int WriteSingleRegisterContent
        {
            get { return _writeSingleRegisterContent; }
            set
            {
                _writeSingleRegisterContent = value; 
                OnPropertyChanged();
            }
        }
        private int _writeSingleRegisterContent;

        #endregion
    }
}
