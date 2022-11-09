import React, { useState, useEffect, useRef, Component, useContext, MouseEventHandler} from 'react'
import reactLogo from './assets/react.svg'
import './App.css'

const ThemeContext = React.createContext<boolean>(false);
//const ThemeUpdateContext = React.createContext<(()=>void) | null>(null);
const ThemeUpdateContext = React.createContext<MouseEventHandler<HTMLButtonElement> | undefined>(undefined);

function useTheme(){
  return useContext(ThemeContext);
}

function useThemeUpdate(){
  return useContext(ThemeUpdateContext);
}

function ThemeProvider({children}: { children: (React.ReactElement | JSX.Element)[] }){
  const [darkTheme, setDarkTheme] = useState(true);
  function toggleTheme() {
    setDarkTheme(prevDarkTheme => !prevDarkTheme)
  }

  return (
    <ThemeContext.Provider value={darkTheme}>
      <ThemeUpdateContext.Provider value={toggleTheme}>
        {children}
      </ThemeUpdateContext.Provider>
    </ThemeContext.Provider>
  );

}


class ClassContextComponent extends Component {
  themeStyles(dark: boolean){
    return {
      backgroundColor: dark? '#333' : '#CCC',
      color: dark ? '#CCC': '#333',
      padding: '2rem',
      margin: '2rem'
    }
  }
  render (){
    return (
      <ThemeContext.Consumer>
        {
          darkTheme => {
            return <div style={this.themeStyles(darkTheme)}>
              Class Theme
            </div>
          }
        }
      </ThemeContext.Consumer>
    );
  }
}

function FunctionContextComponent(){
  //const darkTheme = useContext(ThemeContext);
  const darkTheme = useTheme();
  const toggleTheme = useThemeUpdate();

  const themeStyles = {
    backgroundColor: darkTheme ? '#333':'#CCC',
    color: darkTheme ? '#CCC' : '#333',
    padding: '2rem',
    margin: '2rem'
  };
  return (
    <>
      <button onClick = {toggleTheme}>Toggle Theme</button>
      <div style={themeStyles}>Function Theme</div>
    </>
  );
}

function App() {
  const [darkTheme, setDarkTheme] = useState(true);

  function toggleTheme(){
    setDarkTheme(prevDarkTheme => !prevDarkTheme);
  }

  return (<>
    <ThemeProvider>
      <FunctionContextComponent />
      <ClassContextComponent />
    </ThemeProvider>
  </>);
}

export default App
