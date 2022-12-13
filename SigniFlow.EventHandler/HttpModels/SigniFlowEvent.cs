namespace SigniFlow.EventHandler.HttpModels
{
    public class SigniFlowEvent
    {
        /// <summary>Password/Key that is saved in the Event Handler and SigniFlow, which gets passed on each reqeust in order to validate the server/request.</summary>
        public string SFS { get; set; }

        /// <summary>Event type that took place in SigniFlow</summary>
        public string ET { get; set; }

        /// <summary>DocumentID for the document where the event took place.</summary>
        public string DI { get; set; }

        /// <summary>UserID of the user who actioned the event.</summary>
        public string UI { get; set; }

        /// <summary>Email address of the user who actioned the event.</summary>
        public string UE { get; set; }

        /// <summary>FormID for the form on which the event took place.</summary>
        public string FI { get; set; }

        /// <summary>Form name of the form on which the event took place.</summary>
        public string FN { get; set; }

        /// <summary>Date and time on which event took place.</summary>
        public DateTime ED { get; set; }
    }
}
