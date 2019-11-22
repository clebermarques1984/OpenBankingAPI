using System;
using System.Collections.Generic;
using System.Text;
using Flunt.Notifications;
using Flunt.Validations;

namespace OBAPI.Application.Domain
{
	public abstract class Validatable : Notifiable, IValidatable
	{
		public abstract void Validate();
	}
}
