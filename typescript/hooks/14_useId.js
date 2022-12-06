import React, { useId } from 'react';

export default function EmailForm() {
  const id = useId();

  return (
    <div>
      <label htmlFor={`${id}-email`}>Email</label>
      <input id={`${id}-email`} type="email" />
      <br />
      <label htmlFor={`${id}-name`}>Email</label>
      <input id={`${id}-name`} type="text" />
    </div>
  );
}
/////////////////////////////////////////////////////////
import React from 'react';

import EmailForm from './EmailForm';

import './style.css';
export default function App() {
  return (
    <div>
      <EmailForm />
      <article style={{ margineBlock: '1rem' }}>
        Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor
        sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet
      </article>
      <EmailForm />
    </div>
  );
}
