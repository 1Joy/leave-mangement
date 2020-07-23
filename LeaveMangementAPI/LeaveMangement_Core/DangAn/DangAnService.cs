

using System;
using System.Net.Mail;

namespace LeaveMangement_Core.DangAn
{
    public class DangAnService
    {
        //public object SendAuthCodeMessage(string phone)
        //{
        //    string authCode = GetAuthCode();
        //    var res = new object();
        //    String product = "Dysmsapi";//短信API产品名称
        //    String domain = "";//短信API产品域名
        //    String accessKeyId = "";//你的accessKeyId
        //    String accessKeySecret = "";//你的accessKeySecret
        //    IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
        //    DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
        //    IAcsClient acsClient = new DefaultAcsClient(profile);
        //    SendSmsRequest request = new SendSmsRequest();
        //    try
        //    {
        //        //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
        //        request.PhoneNumbers = phone;
        //        //必填:短信签名-可在短信控制台中找到
        //        request.SignName = "张云鹏";
        //        //必填:短信模板-可在短信控制台中找到
        //        request.TemplateCode = "SMS_133885140";
        //        //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
        //        request.TemplateParam = "您的注册验证码为：${authCode}";
        //        //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
        //        request.OutId = "21212121211";
        //        //请求失败这里会抛ClientException异常
        //        SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
        //        //result表示执行结果，是由阿里云返回给本地服务器的
        //        String result = sendSmsResponse.Message;
        //        res = new
        //        {
        //            result,
        //            authCode
        //        };
        //    }
        //    catch (ServerException e)
        //    {
        //        return e;
        //    }
        //    catch (ClientException e)
        //    {
        //        return e;
        //    }
        //    return res;
        //}
        public object SendAuthCodeEMail(string mailAddress)
        {
            var res = new object();
            try
            {
                string authCode = GetAuthCode();                
                MailMessage eMail = new MailMessage();
                eMail.To.Add(mailAddress);
                eMail.From = new MailAddress("13628471426@163.com", "人事考勤系统", System.Text.Encoding.UTF8);
                eMail.Subject = "验证码邮件";
                eMail.SubjectEncoding = System.Text.Encoding.UTF8;
                eMail.Body = "您的验证码为：" + authCode + "请在两分钟内正确输入此验证码";
                eMail.BodyEncoding = System.Text.Encoding.UTF8;
                eMail.IsBodyHtml = true;
                eMail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient
                {
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential("13628471426@163.com", "jxzxc1230"),
                    Host = "smtp.163.com",
                    Port = 25,
                    EnableSsl = true
                };
                client.Send(eMail);
                res = new
                {
                    isSuccess = true,
                    authCode
                };
            }
            catch
            {
                res = new
                {
                    isSuccess = false
                };
            }
            return res;
        }
        public string GetAuthCode()
        {
            Random rd = new Random();
            //这里生成一个 6 位数的全数字验证码
            int authCodeNumber = rd.Next(100000, 1000000);
            string authCode = authCodeNumber.ToString();
            return authCode;
        }
    }
}
