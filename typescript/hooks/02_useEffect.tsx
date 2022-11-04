import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'

function App() {
  const [resourceType, setResourceType] = useState<string>("posts");
  const [items, setItems] = useState([]);
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);

  useEffect(()=>{
    console.log("resourceType updated");
    return ()=>{
      console.log("clean up for the use effect");
    }
  }, [resourceType]);
  useEffect(()=>{console.log("on mount")},[]);

  useEffect(()=>{
    fetch(`https://jsonplaceholder.typicode.com/${resourceType}`)
    .then(response=>response.json())
    .then(res=>setItems(res))
  },[resourceType]);

  const handleWindowResize = ()=>{
    setWindowWidth(window.innerWidth);
  };
  useEffect(()=>{
    addEventListener('resize', handleWindowResize);
    return ()=>{
      removeEventListener('resize', handleWindowResize);
    };
  },[]);

  return <>
    <h1>width:{windowWidth}</h1>
    <div>
      <button onClick={()=>{setResourceType("posts")}}>Posts</button>
      <button onClick={()=>{setResourceType("users")}}>Users</button>
      <button onClick={()=>{setResourceType("comments")}}>Comments</button>
    </div>
    <h1>{resourceType}</h1>
    {
      items.map(item=>(<pre>{JSON.stringify(item)}</pre>))
    }
  </>
}

export default App
