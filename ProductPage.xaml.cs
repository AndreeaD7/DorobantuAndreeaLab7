namespace DorobantuAndreeaLab7;

public partial class ProductPage : ContentPage
{
    private ShopList _shopList;

    public ProductPage(ShopList shopList)
    {
        InitializeComponent();
        _shopList = shopList;
    }

    // Event handler for adding a product to the shopping list
    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product selectedProduct)
        {
            var listProduct = new ListProduct
            {
                ShopListID = _shopList.ID,
                ProductID = selectedProduct.ID
            };

            await App.Database.SaveListProductAsync(listProduct);

          
            selectedProduct.ListProducts = new List<ListProduct> { listProduct };

            await Navigation.PopAsync(); // Return to the previous page
        }
        else
        {
            await DisplayAlert("Error", "Please select a product to add.", "OK");
        }
    }


    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product product)
        {
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }

    
    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product selectedProduct)
        {
            await App.Database.DeleteProductAsync(selectedProduct);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please select a product to delete.", "OK");
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
}
