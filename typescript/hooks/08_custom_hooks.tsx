// https://stackblitz.com/edit/react-ts-7918ul?file=index.tsx

import React, { Component, useState, useEffect } from 'react';
import { render } from 'react-dom';

function getSavedValue(key: string, initialValue: any) {
  const savedValue = JSON.parse(localStorage.getItem(key));
  if (savedValue) return savedValue;
  if (initialValue instanceof Function) return initialValue();
  return initialValue;
}

function useLocalStorage(key: string, initialValue: any) {
  const [value, setValue] = useState(() => {
    return getSavedValue(key, initialValue);
  });

  useEffect(() => {
    localStorage.setItem(key, JSON.stringify(value));
  }, [value]);

  return [value, setValue];
}

function useUpdateLogger(value) {
  useEffect(() => {
    console.log(value);
  }, [value]);
}

function App() {
  const [name, setName] = useLocalStorage('name', '');
  useUpdateLogger(name);
  return (
    <div>
      <input
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
    </div>
  );
}

render(<App />, document.getElementById('root'));
