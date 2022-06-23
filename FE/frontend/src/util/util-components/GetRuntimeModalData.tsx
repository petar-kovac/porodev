import { ReactNode } from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  formatDate,
  millisToMinutesAndSeconds,
} from 'util/helpers/date-formaters';

export const GetRuntimeModalData = ({
  exceptionHappened,
  executionOutput,
  executionTime,
  executionStart,
}: IRuntimeRsponse): ReactNode => {
  return (
    <div>
      {!exceptionHappened ? (
        <p>Exception has happened : {`${exceptionHappened}`}</p>
      ) : null}
      <p>Execution output : {executionOutput}</p>
      <p>Execution time : {millisToMinutesAndSeconds(executionTime)}</p>
      <p>Execution start : {formatDate(executionStart)}</p>
    </div>
  );
};
