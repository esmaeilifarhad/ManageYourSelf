using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageYourSelfMVC.ViewModels
{
    public class PlayerScoreVM:Models.DomainModels.PlayerScore
    {
        public string PlayerName { get; set; }
        public int SumScore { get; set; }
        public int RowNumber { get; set; }
        public string TeamName { get; set; }
        public float AVGScore { get; set; }
        public int CountVote { get; set; }
        public DataTable VoteToPlayersPIVOT { get; set; }

    }
}