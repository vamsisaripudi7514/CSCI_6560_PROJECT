package database

type Database interface {
	Connect() (Database, error)
}

func (s *SQLClient) Connect() (Database, error) {
	return NewSQLClient()
}

func NewDatabase() Database {
	return &SQLClient{}
}
