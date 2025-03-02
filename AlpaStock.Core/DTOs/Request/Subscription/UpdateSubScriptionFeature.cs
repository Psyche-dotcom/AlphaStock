using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Request.Subscription
{
    public class UpdateSubScriptionFeature
    {
        public string CurrentState { get; set; }
        public string FeatureId { get; set; }
    }
}
