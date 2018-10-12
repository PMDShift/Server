using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Players.Turns
{
    public class CommitmentState
    {
        public ICommitment PendingCommitment { get; private set; }

        public bool HasPendingCommitment
        {
            get { return PendingCommitment != null;  }
        }

        public void QueueCommitment(ICommitment commitment)
        {
            if (this.PendingCommitment != null)
            {
                return;
            }

            this.PendingCommitment = commitment;
        }

        public void CompleteCommitment()
        {
            this.PendingCommitment = null;
        }
    }
}
