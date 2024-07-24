namespace FinPlus.Domain.Users.Drop
{
    public struct DropPassport
    {
        public string Series { get; set; }

        public string Number { get; set; }

        public override string ToString()
        {
            return $"{Series} {Number}";
        }
    }
}
