import * as System from './_include';

export class Type {
	// Type
	constructor() { }

	private __name: string;
	public get Name(): string {
		return this.__name;
	}
	public set Name(value: string) {
		this.__name = value;
	}

	private __namespace: string;
	public get Namespace(): string {
		return this.__namespace;
	}
	public set Namespace(value: string) {
		this.__namespace = value;
	}

	private __fullName: string;
	public get FullName(): string {
		return this.__fullName;
	}
	public set FullName(value: string) {
		this.__fullName = value;
	}

	private __baseType: Type;
	public get BaseType(): Type {
		return this.__baseType;
	}
	public set BaseType(value: Type) {
		this.__baseType = value;
	}

	private __genericTypes: Array<Type> = new Array<Type>();
	public get GenericTypes(): Array<Type> {
		return this.__genericTypes;
	}

	private __properties: Array<System.PropertyInfo> = new Array<System.PropertyInfo>();
	public get Properties(): Array<System.PropertyInfo> {
		return this.__properties;
	}

	public GetProperties(): Array<System.PropertyInfo> {
		let results: Array<System.PropertyInfo> = new Array<System.PropertyInfo>();
		if (this.BaseType) {
			var properties = this.BaseType.GetProperties();
			for (var i = 0; i < properties.length; i++) {
				results.push(properties[i]);
			}
		}
		for (var i = 0; i < this.Properties.length; i++) {
			results.push(this.Properties[i]);
		}

		return results;
	}
	public GetProperty(name: string) {
		let properties: Array<System.PropertyInfo> = this.GetProperties();
		let result: any = properties.find(function (propertyInfo: System.PropertyInfo) {
			return (propertyInfo.Name == name);
		});
		return result;
	}
	public IsSubClassOf(name: string) : boolean {
		let cursor: System.Type = this.BaseType;
		while (cursor != null) {
			if (cursor.FullName == name)
				return true;
			cursor = cursor.BaseType;
		}
		return false;
	}

	public GetConstructor(): any {
		let result: any = System;
		var path = this.Namespace.split('.');
		for (var i = 0; i < path.length; i++) {
			result = result[path[i]];
			if (!result)
				return result;
		}
		result = result[this.Name];
		return result;
	}

	public static GetType(name: string) : System.Type {
		let result: System.Type = new System.Type();

		switch (name) {
			case "any":
			case "string":
			case "number":
			case "boolean":
			case "date":
			case "Array":
			case "Date":
				result.Name = name;
				result.FullName = name;
				return result;
		}

		let match: any = name.match(/^([^<]+)<(.+)>$/);
		if (match) {
			result = Type.GetType(match[1].trim());
			var genericMatches = match[2].match(/([^<|^,]+)(<([^>]+>+))?/g);
			if (genericMatches.length > 0) {
				result.FullName += "<";
				for (var i = 0; i < genericMatches.length; i++) {
					var genericMatch = genericMatches[i];
					var genericType = Type.GetType(genericMatch);
					result.GenericTypes.push(genericType);
					result.FullName += genericType.FullName + ",";
				}
				result.FullName = result.FullName.replace(/,\s*$/, ">");
			}
			return result;
		}

		var _type: any = System;
		var paths = name.split('.');
		for (var i = 0; i < paths.length; i++) {
			_type = _type[paths[i]];
			if (!_type)
				throw "Unable to find definition for " + name;
		}
		_type = _type._type;
		if (! _type)
			throw name + " does not have a definition for _type";

		var names = _type.Name.split('.');
		for (var i = 0; i < names.length; i++) {
			if (i == names.length - 1)
				result.Name = names[i];
			else {
				if (!result.Namespace)
					result.Namespace = "";
				result.Namespace += names[i] + ".";
			}
		}
		if (result.Namespace && result.Namespace.length > 0)
			result.Namespace = result.Namespace.replace(/\.$/, "");
		result.FullName = result.Name;
		if (result.Namespace)
			result.FullName = result.Namespace + "." + result.Name;
		if (_type.BaseType)
			result.BaseType = this.GetType(_type.BaseType);
		if (_type.Properties) {
			for (var i = 0; i < _type.Properties.length; i++) {
				let propertyInfo: System.PropertyInfo = new System.PropertyInfo();
				propertyInfo.Name = _type.Properties[i].Name;
				propertyInfo.PropertyType = this.GetType(_type.Properties[i].PropertyType);
				propertyInfo.Variable = _type.Properties[i].Variable;
				result.Properties.push(propertyInfo);
			}
		}
		return result;
	}
}