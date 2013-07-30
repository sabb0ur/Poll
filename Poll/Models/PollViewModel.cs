using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Poll.Models
{
    public class PollViewModel
    {
        public MobileServiceCollection<Option,Option> Items;
        public MobileServiceClient MobileService = new MobileServiceClient(
            "https://quickvote.azure-mobile.net/",
            "IlgufEcPdtpoTICcTQXnilvugScnMV52"
        );

        public PollViewModel()
        {
            //Items = new ObservableCollection<Option>();
            //for (int i = 0; i < 4; i++)
            //{
            //    AddItem(new Option() { Name = "Item " + i, Count = i });
            //}
        }

        public async Task LoadItems()
        {
            Items = await MobileService.GetTable<Option>().ToCollectionAsync();
        }

        public async Task AddItem(Option option)
        {
           // option.Id = Items.Count;
            await MobileService.GetTable<Option>().InsertAsync(option);
            Items.Add(option);
        }

        public async Task Vote(Option option)
        {
            //Items.First(x => x.Id == option.Id).Count++;
            await MobileService.GetTable<Vote>().InsertAsync(new Vote { OptionId = option.Id } );
        }

        public async Task DeleteVotes()
        {
            await MobileService.InvokeApiAsync("deleteallvotes", HttpMethod.Delete, null);
        }
    }
}
