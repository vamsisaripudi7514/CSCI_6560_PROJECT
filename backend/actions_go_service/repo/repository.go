package repo

import "github.com/vamsisaripudi7514/CSCI_6560_PROJECT/tree/main/backend/actions_go_service/database"

type Repository struct {
	DB database.Database
}

func NewRepository() Repository {
	sqlDB, err := database.NewDatabase().Connect()
	if err != nil {
		panic("failed to connect database")
	}
	return Repository{DB: sqlDB}
}
