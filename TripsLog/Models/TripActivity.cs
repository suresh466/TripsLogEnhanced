namespace TripsLog.Models
{
    // TripActivity class for many-to-many relationship between Trip and Activity
    public class TripActivity
    {
        // implicitly *Id properties are primary keys
        public int TripId { get; set; }
        // Trip property for the relationship
        public Trip Trip { get; set; }

        // connecting activity to trip
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}