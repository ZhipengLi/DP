import React, { useState, useRef, useImperativeHandle } from 'react';
import './style.css';
import ConfirmationModal from './ConfirmationModal';

export default function App() {
  const [open, setOpen] = useState(false);
  const modalRef = useRef();

  return (
    <div>
      <button onClick={() => setOpen(true)}>Open</button>
      <button onClick={() => modalRef.current.focusCloseBtn()}>
        Focus Close
      </button>
      <button onClick={() => modalRef.current.focusConfirmBtn()}>
        Focus Confirm</button>
      <button onClick={() => modalRef.current.focusDenyBtn()}>
      Focus Deny</button>
      <ConfirmationModal
        ref={modalRef}
        isOpen={open}
        onClose={() => setOpen(false)}
      />
    </div>
  );
}

////////////////////////////////////////////////////////////////////////////////

import React, { useImperativeHandle, useRef } from 'react';

function ConfirmationModal({ isOpen, onclose }, ref) {
  const closeRef = useRef();
  const denyRef = useRef();
  const confirmRef = useRef();
  useImperativeHandle(
    ref,
    () => {
      return {
        focusCloseBtn: () => closeRef.current.focus(),
        focusDenyBtn: () => denyRef.current.focus(),
        focusConfirmBtn: () => confirmRef.current.focus(),
      };
    },
    []
  );

  if (!isOpen) return null;
  return (
    <div className="modal">
      <button className="close-btn" ref={closeRef} onClick={onclose}>
        &times;
      </button>
      <div className="modal-header">
        <h1>Title</h1>
      </div>
      <div className="modal-body">Do you confirm?</div>
      <div className="modal-footer">
        <button className="confirm-btn" ref={confirmRef} onClick={onclose}>
          Yes
        </button>
        <button className="deny-btn" ref={denyRef} onClick={onclose}>
          No
        </button>
      </div>
    </div>
  );
}

export default React.forwardRef(ConfirmationModal);
