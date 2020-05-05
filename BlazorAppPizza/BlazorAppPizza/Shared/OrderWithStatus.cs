//using DocumentFormat.OpenXml.Drawing.Charts;
using ComponentsLibrary.map;
using System;
using System.Collections.Generic;
using System.Text;



namespace BlazingPizza.Shared
{
    public class OrderWithStatus
    {
        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch

        public Order Order { get; set; }

        public string StatusText { get; set; }

        public List<marker> Mapmarkers { get; set; }

        public static OrderWithStatus FromOrder(Order order)
        {
            // To simulate a real backend process, we fake status updates based on the amount
            // of time since the order was placed
            string statusText;
            List<marker> mapmarkers;
            var dispatchTime = order.CreatedTime.Add(PreparationDuration);

            if (DateTime.Now < dispatchTime)
            {
                statusText = "Preparing";
                mapmarkers = new List<marker>
                {
                    ToMapmarker("You", order.DeliveryLocation, showPopup: true)
                };
            }
            else if (DateTime.Now < dispatchTime + DeliveryDuration)
            {
                statusText = "Out for delivery";

                var startPosition = ComputeStartPosition(order);
                var proportionOfDeliveryCompleted = Math.Min(1, (DateTime.Now - dispatchTime).TotalMilliseconds / DeliveryDuration.TotalMilliseconds);
                var driverPosition = LatLong.Interpolate(startPosition, order.DeliveryLocation, proportionOfDeliveryCompleted);
                mapmarkers = new List<marker>
                {
                    ToMapmarker("You", order.DeliveryLocation),
                    ToMapmarker("Driver", driverPosition, showPopup: true),
                };
            }
            else
            {
                statusText = "Delivered";
                mapmarkers = new List<marker>
                {
                    ToMapmarker("Delivery location", order.DeliveryLocation, showPopup: true),
                };
            }

            return new OrderWithStatus
            {
                Order = order,
                StatusText = statusText,
                Mapmarkers = mapmarkers,
            };
        }

        private static LatLong ComputeStartPosition(Order order)
        {
            // Random but deterministic based on order ID
            var rng = new Random(order.OrderId);
            var distance = 0.01 + rng.NextDouble() * 0.02;
            var angle = rng.NextDouble() * Math.PI * 2;
            var offset = (distance * Math.Cos(angle), distance * Math.Sin(angle));
            return new LatLong(order.DeliveryLocation.Latitude + offset.Item1, order.DeliveryLocation.Longitude + offset.Item2);
        }

        static marker ToMapmarker(string description, LatLong coords, bool showPopup = false)
        {
            return new marker { Description = description, X = coords.Longitude, Y = coords.Latitude, ShowPopup = showPopup };
        }
    }
}
