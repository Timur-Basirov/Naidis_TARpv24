using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpv24;

public partial class FigurePage : ContentPage
{
	BoxView boxView;
    Ellipse pall;
    Polygon kolmnurk;
	Random rnd = new Random();
	HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
	public FigurePage()
	{
        int r = rnd.Next(256);
        int g = rnd.Next(256);
        int b = rnd.Next(256);
        boxView = new BoxView
        {
            Color = Color.FromRgb(r, g, b),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 30,
        };
        TapGestureRecognizer tap = new TapGestureRecognizer();
        boxView.GestureRecognizers.Add(tap);
        tap.Tapped += (sender, e) =>
        {
            int r = rnd.Next(256);
            int g = rnd.Next(256);
            int b = rnd.Next(256);
            boxView.Color = Color.FromRgb(r, g, b);
            boxView.WidthRequest = boxView.Width + 20;
            boxView.HeightRequest = boxView.Height + 30;
            if(boxView.WidthRequest>(int)DeviceDisplay.MainDisplayInfo.Width/3)
            {
                boxView.WidthRequest = 200;
                boxView.HeightRequest = 200;
            }

        };
        //Ellipse kasutamine
        pall = new Ellipse
        {
            WidthRequest = 200,
            HeightRequest = 200,
            Fill =new SolidColorBrush(Color.FromRgb(b,g,r)),
            Stroke=Colors.BurlyWood,
            StrokeThickness=5,
            HorizontalOptions=LayoutOptions.Center
        };
        TapGestureRecognizer tap_ring = new TapGestureRecognizer();
        tap_ring.NumberOfTapsRequired = 1; //Triple tap
        pall.GestureRecognizers.Add(tap_ring);
        tap_ring.Tapped += (sender, e) =>
        {
            pall.WidthRequest -= 20;
            pall.HeightRequest -= 30;

            if (pall.WidthRequest < 50) 
            {
                pall.WidthRequest = 200;
                pall.HeightRequest = 200;
            }
        };
        //Polygon
        kolmnurk = new Polygon
        {
            Points=new PointCollection
            {
                new Point(0,200), //vasak all
                new Point(100,0),//keskel
                new Point(200,200),//parem all
            },
            Fill = new SolidColorBrush(Color.FromRgb(b, g, r)),
            Stroke = Colors.PeachPuff,
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tap_kolmnurk = new TapGestureRecognizer();
        tap_kolmnurk.NumberOfTapsRequired = 2; //Double tap
        kolmnurk.GestureRecognizers.Add(tap_kolmnurk);
        tap_kolmnurk.Tapped += (sender, e) =>
        {
            //mõtle ise välja
            kolmnurk.Rotation += 30;
            if (kolmnurk.Rotation <= 150)
            {
                kolmnurk.Rotation+= 0;

            }
        };


        hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 18,
                FontFamily = "MinecraftTen",
                TextColor = Colors.White,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                HeightRequest = 40,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { boxView,pall,kolmnurk, hsl },
            HorizontalOptions = LayoutOptions.Center
        };
        Content = vsl;

    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new TextPage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new FigurePage());
        }

    }
}