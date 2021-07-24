
namespace com.jwsoft.common.EncryptDog
{
    public class EncryptDog
    {
        public void IsAuthorization()
        { 
        
        }
    }
}
//namespace com.jwsoft.common.EncryptDog
//{
//    using CodeMeter;
//    using System;
//    using System.Collections;
//    using System.Text;
//    using System.Web;

//    public class EncryptDog
//    {
//        private Api cmApi = new Api();
//        private const int firmCode = 0x187af;
//        private HCMSysEntry hcmEntry = new HCMSysEntry();
//        private const int productCode = 0x1e9bf1;

//        private void ErrorHandler(string line, int ExitCode, HCMSysEntry hcmEntry)
//        {
//            ErrorCodes codes = this.cmApi.CmGetLastErrorCode();
//            if (codes <= ErrorCodes.InvalidHandle)
//            {
//                if ((codes != ErrorCodes.NoError) && (codes == ErrorCodes.InvalidHandle))
//                {
//                    this.cmApi.CmRelease(hcmEntry);
//                    throw new Exception("没有找到加密锁设备");
//                }
//            }
//            else
//            {
//                switch (codes)
//                {
//                    case ErrorCodes.BufferOverflow:
//                        this.cmApi.CmRelease(hcmEntry);
//                        throw new Exception("存储单元中的存储空间不足");

//                    case ErrorCodes.EntryNotFound:
//                        this.cmApi.CmRelease(hcmEntry);
//                        throw new Exception("没有找到存储单元");
//                }
//            }
//        }

//        public HCMSysEntry IsAuthorization()
//        {
//            return null;
//            CmAccess access;
//            access = new CmAccess {
//                Ctrl = CmAccess.Option.NoUserLimit,
//                FirmCode = 0x187af,
//                ProductCode = 0x1e9bf1
//            };
//            if (HttpContext.Current.Session["EncryptDogExcess"] == null)
//            {
//                this.hcmEntry = this.cmApi.CmAccess(CmAccessOption.LocalLan, access);
//                try
//                {
//                    if (this.hcmEntry == null)
//                    {
//                        HttpContext.Current.Response.StatusCode = 0xc7;
//                        Exception exception = new Exception(HttpContext.Current.Response.StatusCode.ToString());
//                        this.cmApi.CmRelease(this.hcmEntry);
//                        throw exception;
//                    }
//                    HttpContext.Current.Session["EncryptDogExcess"] = "success";
//                    this.cmApi.CmRelease(this.hcmEntry);
//                }
//                catch (Exception)
//                {
//                    return null;
//                   // HttpContext.Current.Response.Redirect("/SysFrame/NoAuthentication.aspx");
//                }
//            }
//            return this.hcmEntry;
//        }

//        private bool IsParentDog(CmBoxInfo stCmBoxInfo)
//        {
//            CmAccess acc = new CmAccess();
//            HCMSysEntry hcmse = new HCMSysEntry();
//            bool flag = false;
//            acc.Ctrl = CmAccess.Option.CheckCtsb | CmAccess.Option.NoUserLimit;
//            acc.FirmCode = 0x63;
//            acc.BoxInfo = stCmBoxInfo;
//            hcmse = this.cmApi.CmAccess(CmAccessOption.Local, acc);
//            if (this.cmApi.CmGetLastErrorCode() == ErrorCodes.NoError)
//            {
//                flag = true;
//            }
//            this.cmApi.CmRelease(hcmse);
//            return flag;
//        }

//        private string Read(int infoPosition)
//        {
//            CmAccess acc = new CmAccess();
//            ArrayList list = new ArrayList();
//            acc.Ctrl = CmAccess.Option.NoUserLimit;
//            acc.FirmCode = 0x187af;
//            acc.ProductCode = 0x1e9bf1;
//            HCMSysEntry hcmEntry = this.cmApi.CmAccess(CmAccessOption.LocalLan, acc);
//            this.ErrorHandler("Access", 1, hcmEntry);
//            CmEntryData[] dataArray = (CmEntryData[]) this.cmApi.CmGetInfo(hcmEntry, CmGetInfoOption.EntryData);
//            if (this.cmApi.CmGetLastErrorCode() != ErrorCodes.BufferOverflow)
//            {
//                this.ErrorHandler("CmGetInfo (size)", 2, hcmEntry);
//            }
//            if (dataArray.Length > 0)
//            {
//                this.ErrorHandler("CmGetInfo (data)", 3, hcmEntry);
//                for (uint i = 0; i < dataArray.Length; i++)
//                {
//                    uint ctrl = dataArray[i].Ctrl;
//                    uint num3 = dataArray[i].Ctrl;
//                    uint dataLen = dataArray[i].DataLen;
//                    uint num2 = dataArray[i].Ctrl & 0xffff;
//                    if (num2 <= 0x80)
//                    {
//                        if (num2 != 0x20)
//                        {
//                            if (num2 == 0x40)
//                            {
//                                goto Label_0101;
//                            }
//                            if (num2 == 0x80)
//                            {
//                            }
//                        }
//                    }
//                    else if (((num2 == 0x100) || (num2 == 0x200)) || (num2 == 0x400))
//                    {
//                    }
//                    continue;
//                Label_0101:
//                    list.Add(Encoding.Unicode.GetString(dataArray[i].Data));
//                }
//                this.cmApi.CmRelease(hcmEntry);
//                if (list.Count > infoPosition)
//                {
//                    return list[infoPosition].ToString();
//                }
//            }
//            return "";
//        }

//        public string ReadFirmNameFromEncryptDog()
//        {
//            return this.Read(0);
//        }

//        public string ReadProductVersionFromEncryptDog()
//        {
//            return this.Read(1);
//        }

//        private bool Write(string strInfo, int infoPosition)
//        {
//            uint num = (uint) infoPosition;
//            CmCreatePioExtendedProtectedData data = new CmCreatePioExtendedProtectedData();
//            object pio = new object();
//            byte[] buffer = new byte[0x400];
//            CmProgramUpdateProductItem ctrl = new CmProgramUpdateProductItem();
//            CmCreateItem createItem = new CmCreateItem();
//            CmAccess acc = new CmAccess {
//                Ctrl = CmAccess.Option.NoUserLimit,
//                FirmCode = 0x187af,
//                ProductCode = 0x1e9bf1
//            };
//            HCMSysEntry hcmEntry = this.cmApi.CmAccess(CmAccessOption.Local, acc);
//            this.ErrorHandler("CmAccess", 1, hcmEntry);
//            CmBoxInfo[] infoArray2 = new CmBoxInfo[] { new CmBoxInfo() };
//            CmBoxInfo[] infoArray = this.cmApi.CmGetBoxes(hcmEntry, CmGetBoxesOption.Usb);
//            if (infoArray.Length < 2)
//            {
//                infoArray2 = (CmBoxInfo[]) this.cmApi.CmGetInfo(hcmEntry, CmGetInfoOption.BoxInfo);
//            }
//            else
//            {
//                for (int i = 0; i < infoArray.Length; i++)
//                {
//                    if (!this.IsParentDog(infoArray[i]))
//                    {
//                        infoArray2[0] = infoArray[i];
//                        break;
//                    }
//                }
//            }
//            byte[] bytes = Encoding.Unicode.GetBytes(strInfo);
//            int length = bytes.Length;
//            if (infoArray2 == null)
//            {
//                this.ErrorHandler("CmGetInfo (Boxinfo)", 2, hcmEntry);
//            }
//            CmInternalEntryInfo[] infoArray3 = (CmInternalEntryInfo[]) this.cmApi.CmGetInfo(hcmEntry, CmGetInfoOption.InternalEntryInfo);
//            this.ErrorHandler("CmGetInfo (InternalEntryData)", 3, hcmEntry);
//            CmEntryData[] dataArray1 = (CmEntryData[]) this.cmApi.CmGetInfo(hcmEntry, CmGetInfoOption.EntryData);
//            if ((this.cmApi.CmGetLastErrorCode() != ErrorCodes.BufferOverflow) && (this.cmApi.CmGetLastErrorCode() != ErrorCodes.NoDataAvailable))
//            {
//                this.ErrorHandler("CmGetInfo (EntryData, size)", 4, hcmEntry);
//            }
//            bytes.CopyTo(data.Data, 0);
//            data.DataLen = (ushort) length;
//            data.ExtType = (ushort) num;
//            pio = data;
//            CmAccess access2 = new CmAccess {
//                Ctrl = CmAccess.Option.CheckFsb,
//                FirmCode = 0x63,
//                ProductCode = 10,
//                FeatureCode = 0x20
//            };
//            HCMSysEntry entry2 = new HCMSysEntry();
//            entry2 = this.cmApi.CmAccess(CmAccessOption.Local, access2);
//            this.ErrorHandler("CmAccess", 1, entry2);
//            CmCreatePioProductCode code = new CmCreatePioProductCode {
//                TvbCtrl = (CmCreatePioProductCode.Option) 0,
//                FirmItemReference = infoArray3[0].FirmItemReference,
//                ProductItemReference = infoArray3[0].ProductItemReference,
//                ProductCode = 0x1e9bf1
//            };
//            this.cmApi.CmCreateProductItemOption(entry2, CmCreateProductItemOptionOption.Update, code);
//            this.ErrorHandler("CmCreateProductItemOption (CM_CPIO_UPDATE)", 6, hcmEntry);
//            this.cmApi.CmCreateProductItemOption(entry2, CmCreateProductItemOptionOption.Terminate | CmCreateProductItemOptionOption.Add | CmCreateProductItemOptionOption.ExtendedProtectedData, pio);
//            this.ErrorHandler("CmCreateProductItemOption (CM_CPIO_TERMINATE)", 7, hcmEntry);
//            ctrl.FirmItemReference = infoArray3[0].FirmItemReference;
//            ctrl.ProductItemReference = infoArray3[0].ProductItemReference;
//            createItem.FirmCode = 0x187af;
//            createItem.FirmItemType = infoArray3[0].FirmItemType;
//            createItem.FirmUpdateCounter = infoArray3[0].FirmUpdateCounter;
//            createItem.BoxInfoUser = infoArray2[0];
//            createItem.ProductCode = 0x1e9bf1;
//            byte[] buffer3 = this.cmApi.CmCreateSequence(entry2, GlobalProgrammingOption.UpdateProductItem, ref createItem, ctrl);
//            this.ErrorHandler("CmCreateSequence", 9, hcmEntry);
//            this.cmApi.CmRelease(entry2);
//            this.cmApi.CmProgram(hcmEntry, GlobalProgrammingOption.UpdateProductItem, buffer3);
//            this.ErrorHandler("CmProgram", 10, hcmEntry);
//            this.cmApi.CmRelease(hcmEntry);
//            return true;
//        }

//        public bool WriteFirmNameToEncryptDog(string firmName)
//        {
//            return this.Write(firmName, 0);
//        }

//        public bool WriteProductVersionToEncryptDog(string productVersion)
//        {
//            return this.Write(productVersion, 1);
//        }
//    }
//}

