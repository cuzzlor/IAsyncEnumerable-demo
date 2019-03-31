import React, { Component } from 'react';
import './App.css';
import { HubConnectionBuilder } from '@aspnet/signalr';

interface AppState {
  jokes: string[];
}

class App extends Component<{}, AppState> {

  public state: AppState = {
    jokes: []
  }

  constructor() {
    super({});
    this.getJokes();
  }

  private async getJokes() {
    const connection = new HubConnectionBuilder().withUrl('http://localhost:5000/stream').build();
    await connection.start();
    connection.stream("Jokes", 3000)
      .subscribe({
        next: (joke) => {
          this.state.jokes.push(joke);
          this.setState(this.state);
        },
        complete: () => {
        },
        error: (err) => {
          console.error(err.message, err.stack);
        },
      });
  }

  render() {
    return (
      <ul>
        {this.state.jokes.map(joke => <li key={joke}>{joke}</li>)}
      </ul>
    );
  }
}

export default App;
