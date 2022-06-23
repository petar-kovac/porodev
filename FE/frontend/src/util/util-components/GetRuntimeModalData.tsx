import dayjs from 'dayjs';
import { ReactNode } from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import { formatDate } from 'util/helpers/date-formater';

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
      <p>Execution time : {executionTime}</p>
      <p>Execution start : {formatDate(executionStart)}</p>
    </div>
  );
};
