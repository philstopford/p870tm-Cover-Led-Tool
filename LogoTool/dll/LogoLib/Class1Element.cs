/*
 * Created by SharpDevelop.
 * User: secondofficer
 * Date: 16/12/2020
 * Time: 12:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;   
using System.Configuration;

namespace LogoLib
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class Class1Element : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>Class1Element</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
	
		/// <summary>
		/// A demonstration of how to use a boolean property.
		/// </summary>
		[ConfigurationProperty("special")]
		public bool IsSpecial {
			get { return (bool)this["special"]; }
			set { this["special"] = value; }
		}
	}
	
}

