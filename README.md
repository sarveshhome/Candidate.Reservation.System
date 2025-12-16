# Candidate Selection System

A real-time streaming data processing system for candidate selection with reservation policies using .NET Core and clean architecture.

## Architecture

The solution follows Clean Architecture principles with the following layers:

- **Domain**: Core entities and enums
- **Application**: Business logic and services
- **Infrastructure**: Data access and external services
- **API**: RESTful API endpoints with SignalR for real-time updates
- **Web**: MVC web application with dashboard
- **React**: Modern React frontend with real-time updates

## Features

- ✅ Real-time candidate data stream processing
- ✅ Category-based reservation system (OBC, SC/ST, Women, etc.)
- ✅ Dual reservations for women candidates (WOMAN_OBC, WOMAN_SC_ST)
- ✅ Dynamic cutoff marks calculation
- ✅ Real-time selection results
- ✅ Web dashboard for monitoring
- ✅ RESTful API endpoints
- ✅ SignalR for real-time updates
- ✅ Repository pattern implementation
- ✅ Entity Framework Core with SQL Server
- ✅ Unit tests with xUnit and Moq
- ✅ CI/CD pipelines with GitHub Actions
- ✅ SonarQube integration for code quality

## Reservation Configuration

Default reservation percentages:
- **GENERAL**: 50%
- **OBC**: 27%
- **SC/ST**: 22.5%
- **WOMAN**: 33%
- **WOMAN_OBC**: 15%
- **WOMAN_SC_ST**: 7.5%

## Project Structure

```
candidate-system/
├── src/
│   ├── Candidate.System.Domain/          # Core entities and enums
│   ├── Candidate.System.Application/     # Business logic and services
│   ├── Candidate.System.Infrastructure/  # Data access layer
│   ├── Candidate.System.API/            # REST API with SignalR
│   ├── Candidate.System.Web/            # MVC Web Dashboard
│   └── Candidate.System.React/          # React Frontend Application
├── tests/
│   ├── Candidate.System.Tests.Unit/      # Unit tests with xUnit
│   └── Candidate.System.Tests.Integration/
├── .github/
│   └── workflows/                        # CI/CD pipelines
│       ├── ci.yml
│       ├── cd.yml
│       ├── sonarqube.yml
│       └── build-and-publish-to-release-train.yml
└── Candidate.System.sln
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. **Clone and navigate to the project:**
   ```bash
   cd /Users/sarveshkumar/Practice/NetCore/candidate-system
   ```

2. **Restore packages:**
   ```bash
   dotnet restore
   ```

3. **Run the API (Terminal 1):**
   ```bash
   cd src/Candidate.System.API
   dotnet run
   ```
   API will be available at: `https://localhost:7001`

4. **Run the Web Dashboard (Terminal 2):**
   ```bash
   cd src/Candidate.System.Web
   dotnet run
   ```
   Web Dashboard will be available at: `https://localhost:7002`

5. **Run the React App (Terminal 3):**
   ```bash
   cd src/Candidate.System.React
   npm install
   npm start
   ```
   React App will be available at: `http://localhost:3000`

## API Endpoints

### Candidate Controller

- `POST /api/candidate/submit` - Submit candidates for batch processing
- `POST /api/candidate/process` - Process candidates immediately
- `POST /api/candidate/start-streaming` - Start streaming service
- `POST /api/candidate/stop-streaming` - Stop streaming service

### Example API Usage

```json
POST /api/candidate/process
Content-Type: application/json

[
  {
    "candidateId": "C001",
    "candidateName": "John Doe",
    "category": 1,
    "marks": 85.5,
    "timestamp": "2024-01-15T10:30:00Z"
  },
  {
    "candidateId": "C002",
    "candidateName": "Jane Smith",
    "category": 4,
    "marks": 78.2,
    "timestamp": "2024-01-15T10:31:00Z"
  }
]
```

## Category Codes

- `1` - GENERAL
- `2` - OBC
- `3` - SC_ST
- `4` - WOMAN
- `5` - WOMAN_OBC
- `6` - WOMAN_SC_ST

## Dual Reservation Logic

- **WOMAN_OBC** candidates are eligible for both WOMAN and OBC categories
- **WOMAN_SC_ST** candidates are eligible for both WOMAN and SC_ST categories
- Selection is based on the best available option

## Real-time Features

- **Streaming Processing**: 30-second batch intervals
- **SignalR Hub**: Real-time result updates at `/selectionHub`
- **Web Dashboard**: Live statistics and results

## Configuration

Modify reservation percentages in `SelectionService.cs`:

```csharp
private readonly Dictionary<CandidateCategory, decimal> _reservationConfig = new()
{
    { CandidateCategory.GENERAL, 50m },
    { CandidateCategory.OBC, 27m },
    { CandidateCategory.SC_ST, 22.5m },
    { CandidateCategory.WOMAN, 33m },
    { CandidateCategory.WOMAN_OBC, 15m },
    { CandidateCategory.WOMAN_SC_ST, 7.5m }
};
```

## Testing

### Unit Tests
```bash
dotnet test tests/Candidate.System.Tests.Unit/
```

### Manual Testing
Use the web dashboard to:
1. Add candidates manually
2. Process batches
3. View real-time results
4. Monitor selection statistics

## Technology Stack

- **.NET 9.0**
- **Entity Framework Core** with SQL Server
- **SignalR** for real-time communication
- **Bootstrap** for UI
- **Clean Architecture** pattern
- **Repository Pattern** for data access
- **xUnit** and **Moq** for testing
- **GitHub Actions** for CI/CD
- **SonarQube** for code quality

## Database Setup

```bash
# Run EF migrations
dotnet ef database update --project src/Candidate.System.Infrastructure --startup-project src/Candidate.System.API
```

## CI/CD Pipelines

- **CI Pipeline**: Builds, tests, and runs code quality checks
- **CD Pipeline**: Deploys to production environment
- **SonarQube**: Code quality and security analysis
- **Release Train**: Automated releases with version tagging

## Future Enhancements

- [ ] Email notifications
- [ ] Report generation (PDF/Excel)
- [ ] Admin configuration panel
- [ ] Historical data analysis
- [ ] Performance monitoring
- [ ] Integration tests
- [ ] 

<img width="1507" height="848" alt="Screenshot 2025-12-16 at 5 43 27 PM" src="https://github.com/user-attachments/assets/68c7b10b-e976-4b2c-8ebd-163b57d821d7" />
