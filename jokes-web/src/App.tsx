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

  private getJokes() {
    const connection = new HubConnectionBuilder().withUrl('http://localhost:5000').build();
    connection.on("jokes", (joke: string) => {
      this.state.jokes.push(joke);
      this.setState(this.state);
    })
    connection.start();
  }

  render() {
    return (
      <ul>
        {this.state.jokes.map(joke => <li>{joke}</li>)}
      </ul>
    );
  }
}

export default App;
