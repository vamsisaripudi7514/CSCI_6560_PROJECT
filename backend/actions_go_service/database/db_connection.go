package database

import (
	"log"
	"sync"

	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

type SQLClient struct {
	DB *gorm.DB
}

var (
	SQLClientInstance *SQLClient
	once              sync.Once
)

func NewSQLClient() (*SQLClient, error) {
	var err error
	once.Do(func() {
		dsn := "system_user:MTSU2025@tcp(localhost:3306)/rbac_system?charset=utf8mb4&parseTime=True&loc=Local"
		db, err := gorm.Open(mysql.Open(dsn), &gorm.Config{})
		if err != nil {
			panic("failed to connect database")
		}
		sqlDB, err := db.DB()
		if err != nil {
			panic("failed to connect database")
		}

		err = sqlDB.Ping()
		if err != nil {
			panic("Ping to database failed")
		}
		log.Println("Connected to database")
		SQLClientInstance = &SQLClient{DB: db}

	})
	return SQLClientInstance, err
}
