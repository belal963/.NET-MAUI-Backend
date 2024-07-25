using System;

namespace FindUsHere.General
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string arg) : base(arg) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string arg) :base(arg) { }
    }

    public class InternalServerErorrException : Exception
    {
        public InternalServerErorrException(string arg) : base(arg) { }

    }

    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string arg) : base(arg) { }
    }

}
