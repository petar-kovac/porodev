import { RefObject, useEffect, useRef } from 'react';

interface UseDoubleClickProps {
  ref: RefObject<HTMLElement | null>;
  onDoubleClick?: (event: MouseEvent) => unknown;
  onClick?: (event: MouseEvent) => unknown;
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
      onDoubleClick(event);
    }

    setTimeout(() => {
      if (count.current === 1 && onClick) {
        onClick(event);
      }
      count.current = 0;
    }, 250);
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
