using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOLoader.Settings.News;

namespace UOLoader.Controllers.Events
{
   public class NewsLoadedEventArgs : EventArgs {

      public int NumberOfNews { get; private set; }
      public List<NewsEntry> News { get; private set; }

      public NewsLoadedEventArgs(int number, List<NewsEntry> news) {
         NumberOfNews = number;
         News = news;
      }
   }
}
