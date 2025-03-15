package server

import (
	"log"
	"net/http"

	"github.com/labstack/echo"
	"github.com/vamsisaripudi7514/CSCI_6560_PROJECT/tree/main/backend/actions_go_service/repo"
)

type EchoServer struct {
	echo *echo.Echo
	repo repo.Repository
}

func NewEchoServer() EchoServer {
	repository := repo.NewRepository()
	server := EchoServer{
		echo: echo.New(),
		repo: repository,
	}
	server.registerRoutes()
	return server
}

func (s *EchoServer) registerRoutes() {
	s.echo.GET("/liveness", s.Liveness)
	// cg := s.echo.Group("/coupon")
	// //CRUD operations
	// cg.POST("/create-coupon", s.CreateCoupon)

}

func (s *EchoServer) Test(ctx echo.Context) error {
	return ctx.JSON(http.StatusOK, "Request OK")
}

func (s *EchoServer) Start() error {
	if err := s.echo.Start("0.0.0.0:3003"); err != nil && err != http.ErrServerClosed {
		log.Fatalf("shutting down the server: %v", err)
		return err
	}
	return nil
}

func (s *EchoServer) Stop() error {
	if err := s.echo.Shutdown(nil); err != nil {
		log.Fatalf("shutting down the server: %v", err)
		return err
	}
	return nil
}

func (s *EchoServer) Liveness(ctx echo.Context) error {
	return ctx.String(http.StatusOK, "Server is alive")
}

func StartEchoServer() {
	server := NewEchoServer()
	err := server.Start()
	if err != nil {
		log.Fatalf("Error starting Echo server: %v", err)
	}
}
