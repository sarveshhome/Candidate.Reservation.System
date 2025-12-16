# Candidate Selection System - React with Vite

A modern React application built with Vite for the candidate selection system with real-time updates and reservation management.

## Features

- ✅ Vite for fast development and build
- ✅ React 18 with JSX
- ✅ Real-time updates via SignalR
- ✅ Bootstrap 5 UI
- ✅ Category-based reservation system
- ✅ Yarn package management

## Getting Started

### Prerequisites

- Node.js 16+ and Yarn
- .NET API running on https://localhost:7001

### Installation

```bash
cd src/Candidate.System.React
yarn install
```

### Running the Application

```bash
yarn start
# or
yarn dev
```

The application will be available at: `http://localhost:5173`

## Project Structure

```
src/
├── components/
│   ├── CandidateForm.jsx     # Form to add new candidates
│   ├── SelectionResults.jsx  # Display selected candidates
│   └── ReservationStats.jsx  # Show reservation statistics
├── App.jsx                   # Main application component
└── main.jsx                  # Vite entry point
```

## Technology Stack

- **Vite** - Fast build tool
- **React 18** with JSX
- **Bootstrap 5** for styling
- **SignalR** for real-time communication
- **Axios** for HTTP requests
- **Yarn** for package management

## API Integration

Connects to .NET API at `https://localhost:7001` via Vite proxy configuration.

## Category Codes

- `1` - GENERAL (50%)
- `2` - OBC (27%)
- `3` - SC_ST (22.5%)
- `4` - WOMAN (33%)
- `5` - WOMAN_OBC (15%)
- `6` - WOMAN_SC_ST (7.5%)