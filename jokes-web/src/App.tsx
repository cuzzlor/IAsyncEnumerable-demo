import React, { Component } from 'react';
import './App.css';
import { HubConnectionBuilder } from '@aspnet/signalr';

interface AppProps {
  jokes: {
    delay: number;
    maxListSize: number;
  }
}
interface AppState {
  jokes: string[];
}

class App extends Component<AppProps, AppState> {

  public state: AppState = {
    jokes: []
  }

  constructor(props: AppProps) {
    super(props);
    this.getJokes();
  }

  private async getJokes() {
    const connection = new HubConnectionBuilder().withUrl('http://localhost:5000/stream').build();
    await connection.start();
    connection.stream("Jokes", this.props.jokes.delay)
      .subscribe({
        next: (joke) => {
          this.state.jokes.push(joke);
          this.state.jokes = this.state.jokes.slice(-this.props.jokes.maxListSize);
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
