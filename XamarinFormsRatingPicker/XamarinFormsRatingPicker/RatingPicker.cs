using System;
using Xamarin.Forms;

namespace XamarinFormsRatingPicker
{
    public class RatingPicker : StackLayout
    {
        public RatingPicker()
        {
            this.Padding = new Thickness(0);
            this.Orientation = StackOrientation.Horizontal;

            this.AddStar(1);
            this.AddStar(2);
            this.AddStar(3);
            this.AddStar(4);
            this.AddStar(5);
        }

        private void AddStar(int value)
        {
            var star = new Image { Source = "star_blank", WidthRequest = 48, HeightRequest = 48, };
            var tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) => this.StarTapped(value);
            star.GestureRecognizers.Add(tap);
            this.Children.Add(star);
        }

        public void StarTapped(int star)
        {
            this.Rating = star;
        }

        public static readonly BindableProperty RatingProperty = BindableProperty.Create(
                    nameof(Rating),
                    typeof(int),
                    typeof(RatingPicker),
                    0,
                    BindingMode.TwoWay,
                    propertyChanged: (s, o, n) => { (s as RatingPicker).OnRatingUpdated((int)n); });

        public int Rating
        {
            get => (int)this.GetValue(RatingProperty);
            set
            {
                this.SetValue(RatingProperty, value);
                this.SwitchOnStars();
            }
        }

        public event EventHandler<int> RatingUpdated;

        protected virtual void OnRatingUpdated(int rating)
        {
            this.RatingUpdated?.Invoke(this, rating);
        }

        private void SwitchOnStars()
        {
            var index = 1;
            foreach (var child in this.Children)
            {
                if (child is Image image)
                {
                    var icon = index <= this.Rating ? "star_selected" : "star_blank";
                    image.Source = icon;
                    index++;
                }
            }
        }
    }
}
