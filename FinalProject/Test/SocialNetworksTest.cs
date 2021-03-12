using FinalProject.Enumeration;
using FinalProject.Page;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class SocialNetworksTest : BaseTest
    {
        [Test]
        [TestCase(SocialNetworks.Instagram, "Instagram", TestName = "Check or Instagram opened")]
        [TestCase(SocialNetworks.Twitter, "Twitter", TestName = "Check or Twitter opened")]        
       
        [TestCase(SocialNetworks.YouTube, "YouTube", TestName = "Check or YouTube opened")]
        [TestCase(SocialNetworks.Facebook, "Facebook", TestName = "Check or Facebook opened")]
        public static void BANDYMAS(Enum socialNetwork, string socialNetworkName )
        {
            blacksCoUkPage.NavigateToDafaultPage();
            //blacksCoUkPage.DismissConfirmationAlert(); //kazkas kitas ne alertas
            socialNetworksPage.ClickOnSocialButton(socialNetworkName);
            string browserTitle = socialNetworksPage.OpenSocialNetworksPageAndReturnTitle();
            socialNetworksPage.TestOrSocialNetworksPageOpened( browserTitle, socialNetwork);
            socialNetworksPage.CloseSocialNetworks();
        }
    }
}
