import React from 'react';
import ReactDOM from 'react-dom/client';
import { ErrorBoundary, FallbackProps } from 'react-error-boundary';
import App from './App';
import reportWebVitals from './reportWebVitals';

import Error from './pages/error/ErrorPage';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement,
);
const ErrorFallback: React.FC<FallbackProps> = ({
  error,
  resetErrorBoundary,
}) => {
  return <Error message="UI Error" />;
};

root.render(
  <React.StrictMode>
    <ErrorBoundary FallbackComponent={ErrorFallback}>
      <App />
    </ErrorBoundary>
  </React.StrictMode>,
);

reportWebVitals();
