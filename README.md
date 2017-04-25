To run project please do this:

1) add some files dll like: System.Data.SQLite.dll

2) create databse and create table by call only one time these methods from object (SqliteCalss):
	private  SqliteCalss _db = new SqliteCalss();
	_db.createNewDatabase();
	_db.createTable();

3) run project :)