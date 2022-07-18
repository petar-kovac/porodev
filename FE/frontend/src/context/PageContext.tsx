import {
  createContext,
  Dispatch,
  FC,
  ReactNode,
  SetStateAction,
  useContext,
  useMemo,
  useState,
} from 'react';

type PageContextProps = {
  isLoading: boolean;
  setIsLoading: Dispatch<SetStateAction<boolean>>;
  isModalVisible: boolean;
  setIsModalVisible: Dispatch<SetStateAction<boolean>>;
  isSiderVisible: boolean;
  setIsSiderVisible: Dispatch<SetStateAction<boolean>>;
  projectId: string;
  setProjectId: Dispatch<SetStateAction<string>>;
  modalContent: ReactNode;
  setModalContent: Dispatch<SetStateAction<ReactNode>>;
  isCollapsed: boolean;
  setIsCollapsed: Dispatch<SetStateAction<boolean>>;
};

export const PageContext = createContext<PageContextProps>({
  isLoading: false,
  setIsLoading: () => undefined,
  isModalVisible: false,
  setIsModalVisible: () => undefined,
  isSiderVisible: false,
  setIsSiderVisible: () => undefined,
  projectId: '',
  setProjectId: () => undefined,
  modalContent: undefined,
  setModalContent: () => undefined,
  isCollapsed: false,
  setIsCollapsed: () => undefined,
});

export const PageConsumer = PageContext.Consumer;

const PageProvider: FC<any> = ({ children }) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isSiderVisible, setIsSiderVisible] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [projectId, setProjectId] = useState<string>('');

  const [modalContent, setModalContent] = useState<ReactNode>(undefined);

  const [isCollapsed, setIsCollapsed] = useState<boolean>(
    localStorage.getItem('collapsedMenu') === 'true',
  );

  const state: PageContextProps = useMemo(
    () => ({
      isLoading,
      setIsLoading,
      isModalVisible,
      setIsModalVisible,
      isSiderVisible,
      setIsSiderVisible,
      modalContent,
      setModalContent,
      setProjectId,
      projectId,
      isCollapsed,
      setIsCollapsed,
    }),
    [
      isLoading,
      isModalVisible,
      isSiderVisible,
      modalContent,
      projectId,
      isCollapsed,
    ],
  );
  return <PageContext.Provider value={state}>{children}</PageContext.Provider>;
};

export const usePageContext: () => PageContextProps = () =>
  useContext(PageContext);

export default PageProvider;
