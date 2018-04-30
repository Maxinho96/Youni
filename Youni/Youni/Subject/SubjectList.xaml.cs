using System.Threading.Tasks;

using Xamarin.Forms;

namespace Youni
{
    public partial class SubjectList : ContentPage
	{
		public SubjectList()
		{
			InitializeComponent();

            SubjectListViewModel subjectListViewModel = new SubjectListViewModel();
            subjectListViewModel.Navigation = this.Navigation;
            this.BindingContext = subjectListViewModel;
		}
        public async Task ForceAppearing()
        {
            await Task.Run(() => OnAppearing());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((SubjectListViewModel)this.BindingContext).GetClasses();
        }
    }
}