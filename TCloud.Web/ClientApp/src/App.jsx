import React from 'react';
import {
  HashRouter as Router,
  Route,
  NavLink as Link,
  Switch
} from 'react-router-dom';
import { Nav } from '@ptrampert/flatitude';


class App extends React.Component {
  render = () => {
    return (
      <Router>
        <div className="app left-nav">
          <header>
            <h3>TCLoud</h3>
            <button className="transparent nav-toggle" onClick={() => this.setNavCollapsed(!this.state.navCollapsed)}><i className="fa fa-bars"></i></button>
          </header>
          <Nav>
            <Link to="/" exact activeClassName="current">Dashboard</Link>
            <Link to="/movies" activeClassName="current">Movies</Link>
            <Link to="/music" activeClassName="current">Music</Link>
          </Nav>
          <main>
            <Switch>
              <Route path="/movies">
                Movies
              </Route>
              <Route path="/music">
                Music
              </Route>
              <Route path="/">
                Home
              </Route>
            </Switch>
          </main>
        </div>
      </Router>
    );
  }
}

export default App;