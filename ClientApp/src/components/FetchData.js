import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { results: {}, loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderResults(results) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Amount</th>
            <th>Time Taken</th>
          </tr>
        </thead>
        <tbody>
            <tr key={results.processed}>
              <td>{results.processed}</td>
              <td>{results.elapsedTime}</td>
            </tr>
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderResults(this.state.results);

    return (
      <div>
        <h1 id="tabelLabel" >Widget  stuff</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch("api/widget?amount=15");
    const data = await response.json();
    this.setState({ results: data, loading: false });
  }
}
