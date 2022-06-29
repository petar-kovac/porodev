export interface IPFoldersProps {
  id?: string;
  heading?: string;
  description?: string;
  selected: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
}
