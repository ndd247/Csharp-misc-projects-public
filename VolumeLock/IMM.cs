using System;
using System.Runtime.InteropServices;

namespace VolumeLock
{
    public enum ClsCtx : uint
    {
        InProcServer = 0x1,
        InProcHandler = 0x2,
        All = InProcServer | InProcHandler
    }

    public enum StorageAccessMode
    {
        Read = 0,
        Write = 1,
        ReadWrite = 2
    }

    public enum DataFlow
    {
        Reader = 0,
        Capture = 1,
        All = 2
    }

    [Flags]
    public enum DeviceState
    {
        Active = 0x01,
        Disabled = 0x02,
        NotPresent = 0x04,
        Unplugged = 0x08,
        All = 0x0f
    }

    public enum Role
    {
        Console = 0,
        Multimedia = 1,
        Communications = 2
    }

    public enum AudioShareMode
    {
        Shared,
        Exclusive
    }

    [Flags]
    public enum AudioStreamFlags
    {
        None = 0,
        CrossProcess = 0x00010000,
        Loopback = 0x00020000,
        EventCallback = 0x00040000,
        NoPersist = 0x00080000,
        RateAdjust = 0x00100000,
        ExpireWhenUnowned = 0x10000000,
        DisplayHide = 0x20000000,
        HideWhenExpired = 0x40000000
    }

    [Flags]
    public enum AudioClientBufferFlags
    {
        None = 0x0,
        DataDiscontinuity = 0x1,
        Silent = 0x2,
        TimestampError = 0x4
    }

    public struct PropertyKey
    {
        public Guid FormatId;
        public int PropertyId;

        public PropertyKey(Guid fmtId, int id)
        {
            this.FormatId = fmtId;
            this.PropertyId = id;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct PropertyVariant
    {
        [FieldOffset(0)]
        private ushort type;
        [FieldOffset(2)]
        private ushort reserved1;
        [FieldOffset(4)]
        private ushort reserved2;
        [FieldOffset(6)]
        private ushort reserved3;

        [FieldOffset(8)]
        private byte i1Value;
        [FieldOffset(8)]
        private sbyte ui1Value;
        [FieldOffset(8)]
        private short i2Value;
        [FieldOffset(8)]
        private ushort ui2Value;
        [FieldOffset(8)]
        private int i4Value;
        [FieldOffset(8)]
        private uint ui4Value;
        [FieldOffset(8)]
        private long i8Value;
        [FieldOffset(8)]
        private ulong ui8Value;
        [FieldOffset(8)]
        private float r4Value;
        [FieldOffset(8)]
        private double r8Value;
        [FieldOffset(8)]
        private DateTime dateValue;
        [FieldOffset(8)]
        private bool boolValue;
        [FieldOffset(8)]
        private IntPtr ptrValue;
        [FieldOffset(12)]
        private IntPtr dataValue;

        public VarEnum Type { get { return (VarEnum)type; } }

        public object Value
        {
            get
            {
                return this.GetValue();
            }
        }

        private object GetValue()
        {
            switch ((VarEnum)type)
            {
                case VarEnum.VT_EMPTY:
                case VarEnum.VT_NULL:
                    return null;
                case VarEnum.VT_I1:
                    return i1Value;
                case VarEnum.VT_I2:
                    return i2Value;
                case VarEnum.VT_INT:
                case VarEnum.VT_I4:
                case VarEnum.VT_HRESULT:
                    return i4Value;
                case VarEnum.VT_I8:
                    return i8Value;
                case VarEnum.VT_UI1:
                    return ui1Value;
                case VarEnum.VT_UI2:
                    return ui2Value;
                case VarEnum.VT_UI4:
                case VarEnum.VT_UINT:
                    return ui4Value;
                case VarEnum.VT_UI8:
                    return ui8Value;
                case VarEnum.VT_BOOL:
                    return boolValue;
                case VarEnum.VT_DATE:
                    return dateValue;
                case VarEnum.VT_PTR:
                    return ptrValue;
                case VarEnum.VT_R4:
                    return r4Value;
                case VarEnum.VT_R8:
                    return r8Value;
                case VarEnum.VT_BSTR:
                    return Marshal.PtrToStringBSTR(ptrValue);
                case VarEnum.VT_LPSTR:
                    return Marshal.PtrToStringAnsi(ptrValue);
                case VarEnum.VT_LPWSTR:
                    return Marshal.PtrToStringUni(ptrValue);
                case VarEnum.VT_BLOB:
                    var blob = new byte[i4Value];
                    Marshal.Copy(dataValue, blob, 0, i4Value);
                    return blob;
                default:
                    throw new NotImplementedException("No support for variant type: " + type.ToString());
            }
        }
    }

    [ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        void GetCount(out int count);
        void GetAt(int index, out PropertyKey key);
        void GetValue(ref PropertyKey key, out PropertyVariant value);
        void SetValue(ref PropertyKey key, ref PropertyVariant value);
        void Commit();
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        void Activate(ref Guid id, ClsCtx clsctx, IntPtr activationParams,
            [MarshalAs(UnmanagedType.IUnknown)] out object deviceInterface);
        void OpenPropertyStore(StorageAccessMode access, out IPropertyStore properties);
        void GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        void GetState(out DeviceState state);
    }
    /*
    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int Activate(
            [MarshalAs(UnmanagedType.LPStruct)] Guid interfaceID,
            int classContext,
            IntPtr activationParameter,
            [MarshalAs(UnmanagedType.IUnknown)] out object interfaceObject);
    }
    */

    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        void GetCount(out int numDevices);
        void Item(int deviceNum, out IMMDevice device);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMNotificationClient
    {
        void OnDeviceStateChanged(string deviceId, DeviceState newState);
        void OnDeviceAdded(string deviceId);
        void OnDeviceRemoved(string deviceId);
        void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId);
        void OnPropertyValueChanged(string deviceId, PropertyKey key);
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        void EnumAudioEndpoints(DataFlow dataFlow, DeviceState deviceStates, out IMMDeviceCollection devices);
        void GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice device);
        void GetDevice([MarshalAs(UnmanagedType.LPWStr)] string deviceId, out IMMDevice device);
        void RegisterEndpointNotificationCallback(IMMNotificationClient client);
        void UnregisterEndpointNotificationCallback(IMMNotificationClient client);
    }
    /*
    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        void _VtblGap1_1();
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice mmDevice);
    }
    */

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolume
    {
        int RegisterControlChangeNotify(IntPtr audioEndpointVolumeCallbackHandle);
        int UnregisterControlChangeNotify(IntPtr audioEndpointVolumeCallbackHandle);
        int GetChannelCount(ref uint channelCount);
        int SetMasterVolumeLevel(float levelDB, Guid eventContextGUID);
        int SetMasterVolumeLevelScalar(float level, Guid eventContextGUID);
        int GetMasterVolumeLevel(ref float levelDB);
        int GetMasterVolumeLevelScalar(ref float level);
    }

    public static class MMDeviceEnumeratorFactory
    {
        public static IMMDeviceEnumerator CreateInstance()
        {
            return (IMMDeviceEnumerator)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")));
        }
    }
}
