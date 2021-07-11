using Xamarin.Forms;

namespace RAK.Core.UI.Xam.ReusableViews.GenericChatMessagesResources
{

    /// <summary>
    /// 
    /// </summary>
    public class MessageTemplateSelector : DataTemplateSelector
    {

        public MessageTemplateSelector()
        {
            // Retain instances!
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MessageDTO;
            if (messageVm == null)
                return null;
            return messageVm.IsIncoming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;

    }
}
