using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Youni
{
	public partial class SubjectList : ContentPage
	{
		public SubjectList ()
		{
			InitializeComponent ();
		}
        public async Task ForceAppearing()
        {
            await Task.Run(() => OnAppearing());
        }

        public SubjectList(SubjectListViewModel subjectListViewModel) : this()
        {
            subjectListViewModel.Navigation = this.Navigation;
            this.BindingContext = subjectListViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((SubjectListViewModel)this.BindingContext).GetClasses();
        }

    }
}