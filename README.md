
# Elevator Simulator

## Overview
This is a C#-based Elevator Simulator that uses ASP.NET Core MVC for the backend and Processing.js for real-time visualization on the front-end. The application simulates an elevator system with five floors, tracking states like position, movement, and door status. The UI allows users to interact with the elevator via external and internal controls.

## Features
1. **Elevator State Tracking**:
   - Tracks current floor, movement status, and door state.
   - Displays elevator state in real-time on the UI.

2. **Controls**:
   - **External Controls**: "Call Up" and "Call Down" buttons for floors 2-4, "Call" button for floors 1 and 5.
   - **Internal Controls**: Buttons to select target floors once inside the elevator.

3. **Queue and History Logs**:
   - Displays the current queue of pending requests.
   - Logs and displays the history of processed requests.

4. **Real-Time Visualization**:
   - A Processing.js sketch visually represents the elevator, including movement and door states.

5. **Dynamic Polling**:
   - Uses AJAX polling to update elevator states, queue, and history logs every few seconds.

## Architecture
- **Backend**: ASP.NET Core MVC with services for handling queue management and elevator logic.
- **Frontend**: HTML, JavaScript, and Processing.js for visualization.
- **Real-Time Communication**: Polling using JavaScript to fetch and update the elevator state.
- **Design Patterns**: Implements dependency injection, services, and separation of concerns for modularity.

## File Structure
```
.
├── Controllers/
│   ├── ElevatorApiController.cs      # API Controller for elevator requests
│   ├── ElevatorSimulationController.cs # Main view controller
├── Models/
│   ├── FloorRequest.cs               # Represents a floor request
│   ├── ElevatorState.cs              # Represents the elevator's current state
│   ├── ProcessedRequest.cs           # Represents a logged request
├── Services/
│   ├── ElevatorService.cs            # Handles elevator logic
│   ├── QueueService.cs               # Manages request queue and history
├── Views/
│   ├── ElevatorSimulator/
│       ├── Index.cshtml              # Main UI
├── wwwroot/
│   ├── elevator-sketch.js            # Processing.js sketch
│   ├── scripts.js                    # JavaScript for UI updates
├── Tests/
│   ├── ElevatorApiControllerTests.cs # Unit tests for the API
│   ├── ElevatorServiceTests.cs       # Unit tests for elevator logic
│   ├── QueueServiceTests.cs          # Unit tests for queue service
```

## How to Run

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd ElevatorSimulator
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Access the application in your browser at `http://localhost:5000`.

## Processing.js Visualization
The right-side panel of the UI displays a real-time visualization of the elevator using Processing.js. The sketch dynamically updates based on the elevator's position, movement, and door state.

## Unit Tests
The solution includes NUnit tests for:
- Elevator state transitions
- Queue management
- API request handling

Run the tests using:
```bash
dotnet test
```

## Future Enhancements
- Improve visualization with animations for smoother transitions.
- Add configuration options for elevator parameters (e.g., door open/close time, speed).
- Implement WebSocket support for real-time updates instead of polling.

## Acknowledgments
- **Processing.js** for graphical visualization.
- **NUnit** for unit testing.

## Diagram
![Architecture Diagram](diagram.png)

## License
MIT License
