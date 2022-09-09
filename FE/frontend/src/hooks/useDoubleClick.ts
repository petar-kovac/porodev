import React, { RefObject, useEffect, useRef, MouseEventHandler } from 'react';

interface UseDoubleClickProps {
  ref: RefObject<HTMLElement | null>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
  onClick?: MouseEventHandler<HTMLElement>;
  stopPropagation?: boolean;
}

const useDoubleClick = ({
  ref,
  onDoubleClick,
  onClick,
  stopPropagation,
}: UseDoubleClickProps) => {
  const count = useRef(0);

  const handleClick = (event: MouseEvent) => {
    if (stopPropagation) {
      event.stopPropagation();
    }
    count.current += 1;

    if (count.current === 2 && onDoubleClick) {
      onDoubleClick(
        event as unknown as React.MouseEvent<
          HTMLElement,
          globalThis.MouseEvent
        >,
      );
    }

    setTimeout(() => {
      if (count.current === 1 && onClick) {
        onClick(
          event as unknown as React.MouseEvent<
            HTMLElement,
            globalThis.MouseEvent
          >,
        );
      }
      count.current = 0;
    }, 200);
  };

  useEffect(() => {
    if (ref.current) {
      ref.current.addEventListener('click', handleClick);
    }

    return () => {
      if (ref.current) {
        ref.current.removeEventListener('click', handleClick);
      }
    };
  }, []);
};

export default useDoubleClick;
