using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataHandler.Encoder;
using NUnit.Framework;
using Thinktecture.IdentityModel.Tokens;

namespace Checkout.Server.Unit.Tests.Playgrournd
{
    [TestFixture]
    public class Miscellaneous
    {
        [Test]
        public void GenerateKey()
        {
            string symmetricKeyAsBase64 = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new HmacSigningCredentials(keyByteArray);
            Console.WriteLine(keyByteArray);
            Console.WriteLine(signingKey);
        }
    }
}
