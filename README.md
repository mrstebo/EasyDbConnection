# EasyDbConnection

Easily execute commands without the faff

[![Build status](https://ci.appveyor.com/api/projects/status/k8csv52p2w069geb/branch/master?svg=true)](https://ci.appveyor.com/project/mrstebo/easydbconnection/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/mrstebo/EasyDbConnection/badge.svg?branch=master)](https://coveralls.io/github/mrstebo/EasyDbConnection?branch=master)
[![NuGet Version](https://img.shields.io/nuget/v/EasyDbConnection.svg)](https://www.nuget.org/packages/EasyDbConnection/)

## Usage

```cs
using(var dbConnection = new SqlConnection("CONNECTION_STRING"))
{
    var easyDbConnection = new EasyDbConnection(dbConnection);

    easyDbConnection.Open();

    // Do database stuff

    // Explicitly call close (not needed when `IDbConnection` is wrapped in a using scope)
    easyDbConnection.Close();
}
```

```cs
var rowsAffected = easyDbConnection.ExecuteNonQuery("INSERT INTO products (name) VALUES (@name)", new[]
{
    new DbParam("@name", "My Product")
});

Console.WriteLine("Rows Affected: {0}", rowsAffected);
```

```cs
var scalar = easyDbConnection.ExecuteScalar("SELECT name FROM products WHERE id=@id", new[] {
    new DbParam("@id", 2)
});

Console.WriteLine("Scalar: {0}", scalar);
```

```cs
using (var reader = easyDbConnection.ExecuteReader("SELECT * FROM products"))
{
    while (reader.Read())
    {
        Console.WriteLine("{0}: {1}", reader["id"], reader["name"]);
    }
}
```
