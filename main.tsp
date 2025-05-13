import "@typespec/http";

using Http;
@service(#{ title: "Widget Service" })
namespace WidgetService;

model Widget {
  id: string;
  weight: int32;
  color: "red" | "blue";
}

model WidgetList {
  items: Widget[];
}

@error
model WidgetError {
  code: int32;
  message: string;
}

model AnalyzeResult {
  id: string;
  analysis: string;
}

@route("/widgets")
@tag("Widgets")
interface Widgets {
  /** List widgets */
  @get list(): WidgetList | WidgetError;
  /** Read widgets */
  @get read(@path id: string): Widget | WidgetError;
  /** Create a widget */
  @post create(@body body: Widget): Widget | WidgetError;
  /** Update a widget */
  @patch update(@path id: string, @body body: MergePatchUpdate<Widget>): Widget | WidgetError;
  /** Delete a widget */
  @delete delete(@path id: string): void | WidgetError;

  /** Analyze a widget */
  @route("{id}/analyze") @post analyze(@path id: string): AnalyzeResult | WidgetError;
}
